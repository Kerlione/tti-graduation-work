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

namespace tti_graduation_work.Application.Fields.Commands.DeleteField
{

    public class DeleteFieldCommand : IRequest
    {
        public int SupervisorId { get; set; }
        public int FieldId { get; set; }
    }

    public class DeleteFieldCommandHandler : IRequestHandler<DeleteFieldCommand>
    {
        private IApplicationDbContext _context;

        public DeleteFieldCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteFieldCommand request, CancellationToken cancellationToken)
        {
            var supervisor = await _context.Supervisors.Include(s => s.FieldsOfInterest).FirstOrDefaultAsync(x=>x.Id == request.SupervisorId);

            if (supervisor == null)
            {
                throw new NotFoundException($"Supervisor not found");
            }

            var field = supervisor.FieldsOfInterest.FirstOrDefault(x => x.Id == request.FieldId);

            if (field == null)
            {
                throw new NotFoundException($"Field of interest not found for supervisor");
            }

            _context.FieldsOfInterest.Remove(field);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
