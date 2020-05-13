using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Exceptions;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Domain.Entities;
using tti_graduation_work.Domain.Enums;

namespace tti_graduation_work.Application.Users.Commands.UnlockUser
{

	public class UnlockUserCommand : IRequest
	{
		public int Id { get; set; }
	}

	public class UnlockUserCommandHandler : IRequestHandler<UnlockUserCommand>
	{
		private IApplicationDbContext _context;

		public UnlockUserCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<Unit> Handle(UnlockUserCommand message, CancellationToken cancellationToken)
		{
			var user = await _context.Users.FindAsync(message.Id);

			if (user == null)
			{
				throw new NotFoundException(nameof(User), message.Id);
			}

			user.Status = UserStatus.Active;

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
