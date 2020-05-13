using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Interfaces;

namespace tti_graduation_work.Application.Faculties.Queries
{

    public class GetFacultiesQuery : IRequest<FacultiesVm>
    {
    }

    public class GetFacultiesQueryHandler : IRequestHandler<GetFacultiesQuery, FacultiesVm>
    {
        private IApplicationDbContext _context;

        public GetFacultiesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FacultiesVm> Handle(GetFacultiesQuery message, CancellationToken cancellationToken)
        {
            var entities = await _context.Faculties.Include(prog => prog.Programes).ToListAsync(cancellationToken);

            return new FacultiesVm();
        }
    }
}
