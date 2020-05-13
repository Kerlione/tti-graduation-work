using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Exceptions;
using tti_graduation_work.Application.Common.Interfaces;

namespace tti_graduation_work.Application.Supervisors.Commands.UpdateStudentCount
{

	public class UpdateStudentLimitCommand : IRequest
	{
		public int SupervisorId { get; set; }
		public int Value { get; set; }
	}

	public class UpdateStudentLimitCommandHandler : IRequestHandler<UpdateStudentLimitCommand>
	{
		private IApplicationDbContext _context;

		public UpdateStudentLimitCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Unit> Handle(UpdateStudentLimitCommand request, CancellationToken cancellationToken)
		{
			var supervisor = await _context.Supervisors.FindAsync(request.SupervisorId);

			if (supervisor == null)
			{
				throw new NotFoundException($"Supervisor not found");
			}

			//supervisor.

			return Unit.Value;
		}
	}
}
