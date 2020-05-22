using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Interfaces;

namespace tti_graduation_work.Application.GraduationPapers.Queries.GetPaper
{

    public class GetPaperQuery : IRequest<GraduationPaperDto>
    {
        public int StudentId { get; set; }
    }

    public class GetGraduationPaperQueryHandler : IRequestHandler<GetPaperQuery, GraduationPaperDto>
    {
        private IApplicationDbContext _context;
        private IMapper _mapper;

        public GetGraduationPaperQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GraduationPaperDto> Handle(GetPaperQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.GraduationPapers.Include(p => p.Supervisor).FirstOrDefaultAsync(x => x.StudentId == request.StudentId);

            return _mapper.Map<GraduationPaperDto>(entity);
        }
    }
}
