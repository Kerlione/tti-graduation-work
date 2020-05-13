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

namespace tti_graduation_work.Application.Users.Commands.AuthenticateUser
{

	public class AuthenticateUserCommand : IRequest
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}

	public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand>
	{
		private IApplicationDbContext _context;
		private IExternalAuthenticationService _externalAuthenticationService;
		public AuthenticateUserCommandHandler(IApplicationDbContext context, IExternalAuthenticationService externalAuthenticationService)
		{
			_context = context;
			_externalAuthenticationService = externalAuthenticationService;
		}
		public async Task<Unit> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
		{
			_externalAuthenticationService.Authenticate(new UserModel
			{
				Username = request.Username,
				Password = request.Password
			});

			var entity = await _context.Users.SingleOrDefaultAsync(x => x.Username.Equals(request.Username));

			if(entity == null)
			{
				throw new NotFoundException($"User {request.Username} not found");
			}

			var claims = new List<Dictionary<string, string>>
			{
				// TODO add the required token data
			};

			return Unit.Value;
		}
	}
}
