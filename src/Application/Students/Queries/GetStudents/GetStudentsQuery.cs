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

namespace tti_graduation_work.Application.Students.Queries.GetStudents
{

    public class GetStudentsQuery : IRequest<StudentsVm>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }

    public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, StudentsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStudentsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StudentsVm> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            return new StudentsVm
            {
                Students = await _context.Students.Include(s => s.GraduationPaper).ThenInclude(p=>p.Steps).ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .OrderBy(s => s.Id)
                .ToListAsync(cancellationToken),
                Total = _context.Students.Count()
            };
        }
    }
}
