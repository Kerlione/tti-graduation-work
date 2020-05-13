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

namespace tti_graduation_work.Application.Steps.Commands.SendStepToReviewRequest
{

	public class SendStepToReviewCommand : IRequest
	{
		public int GraduationPaperId { get; set; }
		public int StepId { get; set; }
	}

	public class SendStepToReviewCommandHandler : IRequestHandler<SendStepToReviewCommand>
	{
		private IApplicationDbContext _context;
		public SendStepToReviewCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<Unit> Handle(SendStepToReviewCommand request, CancellationToken cancellationToken)
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

			if (step.StepStatus.Equals(StepStatus.Finished))
			{
				throw new NotSupportedException($"Step status is Finished. Approval request is not allowed");
			}

			step.StepStatus = StepStatus.WaitingApproval;

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
