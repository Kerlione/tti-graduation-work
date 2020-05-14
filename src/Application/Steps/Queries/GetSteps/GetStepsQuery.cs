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
using tti_graduation_work.Application.Common.Exceptions;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Domain.Enums;

namespace tti_graduation_work.Application.Steps.Queries.GetSteps
{

    public class GetStepsQuery : IRequest<StepsVm>
    {
        public int GraduationPaperId { get; set; }
    }

    public class GetStepsQueryHandler : IRequestHandler<GetStepsQuery, StepsVm>
    {
        private IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetStepsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<StepsVm> Handle(GetStepsQuery request, CancellationToken cancellationToken)
        {
            var graduationPaper = await _context.GraduationPapers.FindAsync(request.GraduationPaperId);

            if (graduationPaper == null)
            {
                throw new NotFoundException($"Graduation paper with id {request.GraduationPaperId} is not found");
            }

            var steps = _context.Steps.Where(x => x.GraduationPaperId == request.GraduationPaperId).AsQueryable().ProjectTo<StepDto>(_mapper.ConfigurationProvider);

            return new StepsVm
            {
                Statuses = Enum.GetValues(typeof(StepStatus))
                .Cast<StepStatus>()
                .Select(r => new StepStatusDto { Value = (int)r, Name = r.ToString() })
                .ToList(),
                Types = Enum.GetValues(typeof(PaperStep))
                .Cast<PaperStep>()
                .Select(r => new StepTypeDto { Value = (int)r, Name = r.ToString() })
                .ToList(),
                Steps = await steps.ToListAsync(cancellationToken),
                GradautionPaper = _mapper.Map<GraduationPaperDto>(graduationPaper)
            };
        }
    }
}
