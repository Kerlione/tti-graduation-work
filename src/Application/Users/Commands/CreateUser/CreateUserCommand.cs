﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Domain.Entities;
using tti_graduation_work.Domain.Enums;

namespace tti_graduation_work.Application.Users.Commands.CreateUser
{

	public class CreateUserCommand : IRequest<int>
	{
		public int Role { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}

	public class CommandHandler : IRequestHandler<CreateUserCommand, int>
	{
		private IApplicationDbContext _context;
		private IPasswordHasher _passwordHasher;
		public CommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher)
		{
			_context = context;
			_passwordHasher = passwordHasher;
		}
		public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var entity = new User
			{
				Role = (Role)request.Role,
				Status = UserStatus.Active,
				Username = request.Username,
				Password = _passwordHasher.Hash(request.Password)
			};

			_context.Users.Add(entity);

			await _context.SaveChangesAsync(cancellationToken);

			return entity.Id;
		}
	}
}
