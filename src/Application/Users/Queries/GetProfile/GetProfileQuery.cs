using AutoMapper;
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

namespace tti_graduation_work.Application.Users.Queries.GetProfile
{

    public class GetProfileQuery : IRequest<ProfileVm>
    {
        public string Username { get; set; }
    }

    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, ProfileVm>
    {
        private IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetProfileQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ProfileVm> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username.Equals(request.Username));

            if (user == null)
            {
                throw new NotFoundException($"User {request.Username} not found");
            }

            switch (user.Role)
            {
                case Domain.Enums.Role.Student:
                    {
                        var entity = await _context.Students.Include(s => s.User).Include(s => s.Programe).ThenInclude(p => p.Faculty).FirstOrDefaultAsync(x => x.UserId == user.Id);
                        return _mapper.Map<ProfileVm>(entity);
                    }
                case Domain.Enums.Role.Supervisor:
                    {
                        var entity = await _context.Supervisors.Include(s => s.User).Include(s => s.Faculty).FirstOrDefaultAsync(x => x.UserId == user.Id);
                        return _mapper.Map<ProfileVm>(entity);

                    }
                case Domain.Enums.Role.Administrator:
                    {
                        return _mapper.Map<ProfileVm>(user);
                    }
                default:
                    {
                        throw new NotSupportedException($"Role with id {(int)user.Role} is not supported");
                    }
            }
        }
    }
}
