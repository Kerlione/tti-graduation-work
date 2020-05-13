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

namespace tti_graduation_work.Application.Fields.Commands.UpdateField
{

    public class UpdateFieldCommand : IRequest
    {
        public int SupervisorId { get; set; }
        public int FieldId { get; set; }
        public string Title_RU { get; set; }
        public string Title_EN { get; set; }
        public string Title_LV { get; set; }
    }

    public class UpdateFieldCommandHandler : IRequestHandler<UpdateFieldCommand>
    {
        private IApplicationDbContext _context;

        public UpdateFieldCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateFieldCommand request, CancellationToken cancellationToken)
        {
            var supervisor = await _context.Supervisors.Include(s => s.FieldsOfInterest).FirstOrDefaultAsync(x => x.Id == request.SupervisorId);

            if (supervisor == null)
            {
                throw new NotFoundException($"Supervisor not found");
            }

            var field = supervisor.FieldsOfInterest.FirstOrDefault(x => x.Id == request.FieldId);

            if (field == null)
            {
                throw new NotFoundException($"Field of interest not found");
            }

            field.Title_EN = request.Title_EN;
            field.Title_LV = request.Title_LV;
            field.Title_RU = request.Title_RU;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
