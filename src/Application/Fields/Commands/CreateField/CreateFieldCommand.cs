using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Exceptions;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Fields.Commands.CreateField
{

    public class CreateFieldCommand : IRequest<int>
    {
        public int SupervisorId { get; set; }
        public string Title_RU { get; set; }
        public string Title_EN { get; set; }
        public string Title_LV { get; set; }
    }

    public class CreateFieldCommandHandler : IRequestHandler<CreateFieldCommand, int>
    {
        private IApplicationDbContext _context;

        public CreateFieldCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateFieldCommand request, CancellationToken cancellationToken)
        {
            var supervisor = await _context.Supervisors.FindAsync(request.SupervisorId);

            if (supervisor == null)
            {
                throw new NotFoundException($"No supervisor with id {request.SupervisorId} is found");
            }

            var entity = new FieldOfInterest
            {
                SupervisorId = request.SupervisorId,
                Title_EN = request.Title_EN,
                Title_LV = request.Title_LV,
                Title_RU = request.Title_RU
            };

            _context.FieldsOfInterest.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
