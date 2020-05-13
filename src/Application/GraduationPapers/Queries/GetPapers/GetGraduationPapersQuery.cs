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
using tti_graduation_work.Domain.Enums;

namespace tti_graduation_work.Application.GraduationPapers.Queries.GetPapers
{

    public class GetGraduationPapersQuery : IRequest<GraduationPapersVm>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }

    public class GetGraduationPapersQueryHandler : IRequestHandler<GetGraduationPapersQuery, GraduationPapersVm>
    {
        private IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetGraduationPapersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GraduationPapersVm> Handle(GetGraduationPapersQuery request, CancellationToken cancellationToken)
        {
            return new GraduationPapersVm
            {
                GraduationPapers = await _context.GraduationPapers
                .Include(g => g.Student)
                .Include(g => g.Supervisor)
                .Skip(request.Skip)
                .Take(request.Take)
                .ProjectTo<GraduationPaperDto>(_mapper.ConfigurationProvider)
                .ToListAsync(),
                Total = await _context.GraduationPapers.CountAsync(cancellationToken),
                PaperStatuses = Enum.GetValues(typeof(PaperStatus))
                .Cast<PaperStatus>()
                .Select(r => new PaperStatusDto { Value = (int)r, Name = r.ToString() })
                .ToList(),
                PaperTypes = Enum.GetValues(typeof(PaperType))
                .Cast<PaperType>()
                .Select(r => new PaperTypeDto { Value = (int)r, Name = r.ToString() })
                .ToList(),
            };
        }
    }
}
