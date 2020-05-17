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

namespace tti_graduation_work.Application.Steps.Commands.RejectStep
{

	public class RejectStepCommand : IRequest
	{
		public int StepId { get; set; }
		public int GraduationPaperId { get; set; }
		public string Reason { get; set; }
		public bool DeassignSupervisor { get; set; }
	}

	public class RejectStepCommandHandler : IRequestHandler<RejectStepCommand>
	{
		private IApplicationDbContext _context;

		public RejectStepCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Unit> Handle(RejectStepCommand request, CancellationToken cancellationToken)
		{
			var graduationPaper = await _context.GraduationPapers.FindAsync(request.GraduationPaperId);
			if (graduationPaper == null)
			{
				throw new NotFoundException($"Graduation paper with id {request.GraduationPaperId} not found");
			}
			var step = await _context.Steps.FindAsync(request.StepId);

			if (step == null)
			{
				throw new NotFoundException($"Step with id {request.StepId} not found");
			}

			if (!graduationPaper.Steps.Any(s => s.Id == request.StepId))
			{
				throw new NotAccessibleEntityException($"Step with id {request.StepId} is not assigned to graduation paper with id {request.GraduationPaperId}");
			}

			if (step.StepStatus.Equals(StepStatus.Finished) || step.StepStatus.Equals(StepStatus.Rejected) || step.StepStatus.Equals(StepStatus.Approved))
			{
				throw new NotSupportedException($"Step status is {Enum.GetName(typeof(StepStatus), step.StepStatus)} Finishing is not allowed");
			}

			step.StepStatus = StepStatus.Rejected;
			step.Comment = request.Reason;
			if (request.DeassignSupervisor)
			{
				graduationPaper.SupervisorId = null;
			}

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
