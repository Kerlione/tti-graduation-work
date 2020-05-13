using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Users.Queries.GetUser
{

	public class GetUserQuery : IRequest<User>
	{
		public string Username { get; set; }
	}

	public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
	{
		private IApplicationDbContext _context;

		public GetUserQueryHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<User> Handle(GetUserQuery message, CancellationToken cancellationToken)
		{
			return await _context.Users.FirstOrDefaultAsync(x => x.Username.Equals(message.Username));
		}
	}
}
