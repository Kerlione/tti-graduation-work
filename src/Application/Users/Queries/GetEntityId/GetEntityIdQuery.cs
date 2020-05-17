using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Exceptions;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Domain.Enums;

namespace tti_graduation_work.Application.Users.Queries.GetEntityId
{

	public class GetEntityIdQuery : IRequest<int>
	{
		public int UserId { get; set; }
	}

	public class GetEntityIdQueryHandler : IRequestHandler<GetEntityIdQuery, int>
	{
		private IApplicationDbContext _context;

		public GetEntityIdQueryHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<int> Handle(GetEntityIdQuery request, CancellationToken cancellationToken)
		{
			var user = await _context.Users.FindAsync(request.UserId);
			var entityId = 0;
			if(user == null)
			{
				throw new NotFoundException($"User with id {request.UserId} not found!");
			}

			switch (user.Role)
			{
				case Role.Student:
					{
						entityId = _context.Students.FirstOrDefault(x => x.UserId == request.UserId).Id;
						break;
					}
				case Role.Supervisor:
					{
						entityId = _context.Supervisors.FirstOrDefault(x => x.UserId == request.UserId).Id;
						break;
					}
				default:
					{
						throw new Exception($"Role {user.Role} doesn't have a sub-entity");
					}
			}
			return entityId;
		}
	}
}
