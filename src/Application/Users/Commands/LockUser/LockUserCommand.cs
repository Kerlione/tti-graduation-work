﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Exceptions;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Domain.Entities;
using tti_graduation_work.Domain.Enums;

namespace tti_graduation_work.Application.Users.Commands.LockUser
{

	public class LockUserCommand : IRequest
	{
		public int Id { get; set; }
	}

	public class LockUserCommandHandler : IRequestHandler<LockUserCommand>
	{
		private IApplicationDbContext _context;

		public LockUserCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Unit> Handle(LockUserCommand message, CancellationToken cancellationToken)
		{
			var user = await _context.Users.FindAsync(message.Id);

			if (user == null)
			{
				throw new NotFoundException(nameof(User), message.Id);
			}

			user.Status = UserStatus.Locked;

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
