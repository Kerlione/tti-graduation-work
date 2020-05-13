using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Exceptions;
using tti_graduation_work.Application.Common.Interfaces;

namespace tti_graduation_work.Application.Topics.Commands.DeleteTopic
{

	public class DeleteTopicCommand : IRequest
	{
		public int SupervisorId { get; set; }
		public int TopicId { get; set; }
	}

	public class DeleteTopicCommandHandler : IRequestHandler<DeleteTopicCommand>
	{
		private IApplicationDbContext _context;

		public DeleteTopicCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Unit> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
		{
			var supervisor = await _context.Supervisors.Include(s => s.ThesisTopics).FirstOrDefaultAsync(x => x.Id == request.SupervisorId);

			if (supervisor == null)
			{
				throw new NotFoundException($"Supervisor not found");
			}

			var thesisTopic = supervisor.ThesisTopics.FirstOrDefault(x => x.Id == request.TopicId);

			if (thesisTopic == null)
			{
				throw new NotFoundException($"Thesis topic not found for supervisor");
			}

			_context.ThesisTopics.Remove(thesisTopic);

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
