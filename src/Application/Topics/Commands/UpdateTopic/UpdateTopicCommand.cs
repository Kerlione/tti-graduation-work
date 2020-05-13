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

namespace tti_graduation_work.Application.Topics.Commands.UpdateTopic
{

    public class UpdateTopicCommand : IRequest
    {
        public int SupervisorId { get; set; }
        public int TopicId { get; set; }
        public string Title_RU { get; set; }
        public string Title_EN { get; set; }
        public string Title_LV { get; set; }
    }

    public class UpdateTopicCommandHandler : IRequestHandler<UpdateTopicCommand>
    {
        private IApplicationDbContext _context;

        public UpdateTopicCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
        {
            var supervisor = await _context.Supervisors.Include(s => s.ThesisTopics).FirstOrDefaultAsync(x=>x.Id == request.SupervisorId);

            if (supervisor == null)
            {
                throw new NotFoundException($"Supervisor not found");
            }

            var thesisTopic = supervisor.ThesisTopics.FirstOrDefault(x => x.Id == request.TopicId);

            if (thesisTopic == null)
            {
                throw new NotFoundException($"Thesis topic not found");
            }

            thesisTopic.Title_EN = request.Title_EN;
            thesisTopic.Title_LV = request.Title_LV;
            thesisTopic.Title_RU = request.Title_RU;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
