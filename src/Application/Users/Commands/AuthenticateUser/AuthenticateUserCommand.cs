using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Exceptions;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Application.Common.Models;
using tti_graduation_work.Domain.Entities;
using tti_graduation_work.Domain.Enums;

namespace tti_graduation_work.Application.Users.Commands.AuthenticateUser
{

	public class AuthenticateUserCommand : IRequest<UserIdentity>
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}

	public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, UserIdentity>
	{
		private IApplicationDbContext _context;
		private IExternalAuthenticationService _externalAuthenticationService;
		private IAuthRepository _authRepository;
		private IPasswordHasher _passwordHasher;
		public AuthenticateUserCommandHandler(IApplicationDbContext context, IExternalAuthenticationService externalAuthenticationService, IAuthRepository authRepository, IPasswordHasher passwordHasher)
		{
			_context = context;
			_externalAuthenticationService = externalAuthenticationService;
			_authRepository = authRepository;
			_passwordHasher = passwordHasher;
		}
		public async Task<UserIdentity> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
		{
			var entity = await _context.Users.SingleOrDefaultAsync(x => x.Username.Equals(request.Username));

			if (entity == null)
			{
				throw new NotFoundException($"User {request.Username} not found");
			}

			if(entity.Status == UserStatus.Locked)
			{
				throw new UserLockedException(entity);
			}

			//if(entity.Role != Role.Administrator)
			//{
			//	var result = _externalAuthenticationService.AuthenticateAsync(new UserModel
			//	{
			//		Username = request.Username,
			//		Password = request.Password
			//	});

			//	if(result == null)
			//	{
			//		return null;
			//	}
			//}

			var passwordCheck = _passwordHasher.Check(entity.Password, request.Password);
			if (!passwordCheck.Verified)
			{
				return null;
			}

			var profile = GetUserProfile(entity);

			return new UserIdentity
			{
				Token = _authRepository.CreateToken(profile)
			};
		}
		private ProfileData GetUserProfile(User user)
		{
			var profile = new ProfileData
			{
				User = user
			};

			var givenName = string.Empty;
			if (user.Role == Role.Administrator)
			{
				profile.GivenName = user.Username;
				profile.ProfileId = user.Id;
			}
			else
			{
				if (user.Role == Role.Student)
				{
					var student = _context.Students.FirstOrDefault(x => x.UserId == user.Id);
					profile.GivenName = $"{student.Name} {student.Surname}";
					profile.ProfileId = student.Id;
				}
				else
				{
					var supervisor = _context.Supervisors.FirstOrDefault(x => x.UserId == user.Id);
					profile.GivenName = $"{supervisor.Name} {supervisor.Surname}";
					profile.ProfileId = supervisor.Id;
				}
			}

			return profile;
		}
	}	
}
