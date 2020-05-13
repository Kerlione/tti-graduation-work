using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Exceptions;
using tti_graduation_work.Application.Common.Interfaces;

namespace tti_graduation_work.Application.Supervisors.Queries.GetSupervisor
{

    public class GetSupervisorQuery : IRequest<SupervisorDto>
    {
        public int Id { get; set; }
    }

    public class GetSupervisorQueryHandler : IRequestHandler<GetSupervisorQuery, SupervisorDto>
    {
        private IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetSupervisorQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<SupervisorDto> Handle(GetSupervisorQuery request, CancellationToken cancellationToken)
        {
            var supervisor = await _context.Supervisors
                .Include(s => s.SupervisorLanguages).ThenInclude(sl => sl.Language)
                .Include(s => s.JobPosition)
                .Include(s => s.Faculty)
                .Include(s => s.ThesisTopics)
                .Include(s => s.FieldsOfInterest)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (supervisor == null)
            {
                throw new NotFoundException($"Supervisor not found");
            }

            return _mapper.Map<SupervisorDto>(supervisor);
        }
    }
}
