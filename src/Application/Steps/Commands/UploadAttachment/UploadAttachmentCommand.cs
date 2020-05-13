using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Exceptions;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Steps.Commands.UploadAttachment
{

	public class UploadAttachmentCommand : IRequest<int>
	{
		public int GraduationPaperId { get; set; }
		public int StepId { get; set; }
		public string Name { get; set; }
		public string Data { get; set; }
	}

	public class UploadAttachmentCommandHandler : IRequestHandler<UploadAttachmentCommand, int>
	{
		private IApplicationDbContext _context;
		public UploadAttachmentCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<int> Handle(UploadAttachmentCommand request, CancellationToken cancellationToken)
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

			var entity = new Attachment
			{
				StepId = request.StepId,
				Name = request.Name,
				Content = Convert.FromBase64String(request.Data)
			};

			_context.Attachements.Add(entity);

			await _context.SaveChangesAsync(cancellationToken);

			return entity.Id;
		}
	}
}
