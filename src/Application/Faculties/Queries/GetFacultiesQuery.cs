using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        public int Take { get; set; }
        public int Skip { get; set; }
    }

    public class GetFacultiesQueryHandler : IRequestHandler<GetFacultiesQuery, FacultiesVm>
    {
        private IApplicationDbContext _context;
        private IMapper _mapper;

        public GetFacultiesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FacultiesVm> Handle(GetFacultiesQuery message, CancellationToken cancellationToken)
        {
            if(message.Take == 0)
            {
                message.Take = 15;
            }

            return new FacultiesVm
            {
                Faculties = await _context.Faculties.Include(prog => prog.Programes)
                    .ProjectTo<FacultyDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
