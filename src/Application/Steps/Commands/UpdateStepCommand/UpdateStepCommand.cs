using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Exceptions;
using tti_graduation_work.Application.Common.Interfaces;

namespace tti_graduation_work.Application.Steps.Commands.UpdateStepCommand
{

	public class UpdateStepCommand : IRequest
	{
		public int GraduationPaperId { get; set; }
		public int StepId { get; set; }
		public string Data { get; set; }
	}

	public class UpdateStepCommandHandler : IRequestHandler<UpdateStepCommand>
	{
		private IApplicationDbContext _context;
		public UpdateStepCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<Unit> Handle(UpdateStepCommand request, CancellationToken cancellationToken)
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

			step.StepData = request.Data;

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
