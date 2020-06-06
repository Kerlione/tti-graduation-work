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

namespace tti_graduation_work.Application.Users.Queries.GetUsers
{

    public class GetUsersQuery : IRequest<UsersVm>
    {
        public int Take { get; set; }
        public int Skip { get; set; }
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, UsersVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UsersVm> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return new UsersVm
            {
                Roles = Enum.GetValues(typeof(Role))
                .Cast<Role>()
                .Select(r => new RoleDto { Value = (int)r, Name = r.ToString() })
                .ToList(),
                Statuses = Enum.GetValues(typeof(UserStatus))
                .Cast<UserStatus>()
                .Select(r => new StatusDto { Value = (int)r, Name = r.ToString() })
                .ToList(),
                Users = await _context.Users.Take(request.Take).Skip(request.Skip)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .OrderBy(u => u.Username).AsNoTracking()
                .ToListAsync(cancellationToken)
            };

        }
    }
}
