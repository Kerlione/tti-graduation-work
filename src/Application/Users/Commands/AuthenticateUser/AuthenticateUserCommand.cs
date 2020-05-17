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
		public AuthenticateUserCommandHandler(IApplicationDbContext context, IExternalAuthenticationService externalAuthenticationService, IAuthRepository authRepository)
		{
			_context = context;
			_externalAuthenticationService = externalAuthenticationService;
			_authRepository = authRepository;
		}
		public async Task<UserIdentity> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
		{
			var entity = await _context.Users.SingleOrDefaultAsync(x => x.Username.Equals(request.Username));

			if (entity == null)
			{
				throw new NotFoundException($"User {request.Username} not found");
			}

			if(entity.Role != Role.Administrator)
			{
				var result = _externalAuthenticationService.AuthenticateAsync(new UserModel
				{
					Username = request.Username,
					Password = request.Password
				});

				if(result == null)
				{
					return null;
				}
			}

			return new UserIdentity
			{
				Token = _authRepository.CreateToken(entity)
			};
		}
	}
}
