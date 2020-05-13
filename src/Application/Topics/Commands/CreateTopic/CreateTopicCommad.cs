using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Exceptions;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Topics.Commands.CreateTopic
{

    public class CreateTopicCommand : IRequest<int>
    {
        public int SupervisorId { get; set; }
        public string Title_RU { get; set; }
        public string Title_EN { get; set; }
        public string Title_LV { get; set; }
    }

    public class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommand, int>
    {
        private IApplicationDbContext _context;

        public CreateTopicCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var supervisor = await _context.Supervisors.FindAsync(request.SupervisorId);

            if (supervisor == null)
            {
                throw new NotFoundException($"Supervisor not found");
            }

            var entity = new ThesisTopic
            {
                SupervisorId = request.SupervisorId,
                Title_EN = request.Title_EN,
                Title_LV = request.Title_LV,
                Title_RU = request.Title_RU
            };

            _context.ThesisTopics.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
