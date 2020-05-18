using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Interfaces;

namespace tti_graduation_work.Application.Supervisors.Queries.GetSupervisors
{

    public class GetSupervisorsQuery : IRequest<SupervisorsVm>
    {
        public int Take { get; set; }
        public int Skip { get; set; }
    }

    public class GetSupervisorsQueryHandler : IRequestHandler<GetSupervisorsQuery, SupervisorsVm>
    {
        private IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSupervisorsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SupervisorsVm> Handle(GetSupervisorsQuery request, CancellationToken cancellationToken)
        {
            return new SupervisorsVm
            {
                Supervisors = await _context.Supervisors.Include(s => s.Faculty).Take(request.Take).Skip(request.Skip)
                .ProjectTo<SupervisorDto>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken),
                Total = _context.Supervisors.Count()
            };
        }
    }
}
