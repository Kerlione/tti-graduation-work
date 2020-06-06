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

namespace tti_graduation_work.Application.Steps.Queries.GetAvailableSupervisors
{

	public class GetAvailableSupervisorsQuery : IRequest<SupervisorsVm>
	{
	}

	public class GetAvailableSupervisorsQueryHandler : IRequestHandler<GetAvailableSupervisorsQuery, SupervisorsVm>
	{
		private IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetAvailableSupervisorsQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<SupervisorsVm> Handle(GetAvailableSupervisorsQuery request, CancellationToken cancellationToken)
		{
			var availableSupervisors = _context.Supervisors.Include(s => s.GraduationPapers).Where(x => x.GraduationPapers.Count < x.StudentLimit).AsNoTracking();
			return new SupervisorsVm
			{
				List = await availableSupervisors.ProjectTo<SupervisorSm>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken)
			};
		}
	}
}
