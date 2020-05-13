using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Interfaces;

namespace tti_graduation_work.Application.Students.Queries.StudentExists
{

	public class StudentExistsCommand : IRequest<bool>
	{
		public int ExternalId { get; set; }
	}

	public class StudentExistsCommandHandler : IRequestHandler<StudentExistsCommand, bool>
	{
		private readonly IApplicationDbContext _context;

		public StudentExistsCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(StudentExistsCommand request, CancellationToken cancellationToken)
		{
			return await _context.Students.AnyAsync(x => x.ExternalId == request.ExternalId);
		}
	}
}
