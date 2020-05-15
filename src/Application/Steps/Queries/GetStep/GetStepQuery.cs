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

namespace tti_graduation_work.Application.Steps.Queries.GetStep
{

    public class GetStepQuery : IRequest<StepDto>
    {
        public int GraduationPaperId { get; set; }
        public int StepId { get; set; }
    }

    public class GetStepQueryHandler : IRequestHandler<GetStepQuery, StepDto>
    {
        private IApplicationDbContext _context;
        private IMapper _mapper;

        public GetStepQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<StepDto> Handle(GetStepQuery request, CancellationToken cancellationToken)
        {
            var graduationPaper = await _context.GraduationPapers.FindAsync(request.GraduationPaperId);
            if (graduationPaper == null)
            {
                throw new NotFoundException($"Graduation paper with id {request.GraduationPaperId} not found");
            }
            var step = await _context.Steps.FindAsync(request.StepId);

            if (step == null)
            {
                throw new NotFoundException($"Step with id {request.StepId} not found");
            }

            if (!graduationPaper.Steps.Any(s => s.Id == request.StepId))
            {
                throw new NotAccessibleEntityException($"Step with id {request.StepId} is not assigned to graduation paper with id {request.GraduationPaperId}");
            }

            var attachments = _context.Attachements.Where(x=>x.StepId == request.StepId).ProjectTo<AttachmentDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);


            return new StepDto
            {
                Attachments = await attachments,
                Data = step.StepData,
                Id = step.Id,
                StepType = (int)step.StepType,
                StepStatus = (int)step.StepStatus
            };
        }
    }
}
