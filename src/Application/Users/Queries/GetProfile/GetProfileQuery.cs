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

            ProfileVm profileData = new ProfileVm();

            switch (user.Role)
            {
                case Domain.Enums.Role.Student:
                    {
                        var entity = await _context.Students.FirstOrDefaultAsync(x => x.UserId == user.Id);
                        profileData = new ProfileVm
                        {
                            Emails = new List<string>()
                            {
                                entity.Email1,
                                entity.Email2
                            },
                            PhoneNumbers = new List<string>()
                            {
                                entity.Phone1,
                                entity.Phone2
                            },
                            Faculty = entity.Programe.Faculty.Title_EN,
                            FirstName = entity.Name,
                            LastName = entity.Surname,
                            Programe = entity.Programe.Title_EN
                        };
                        break;
                    }
                case Domain.Enums.Role.Supervisor:
                    {
                        var entity = await _context.Supervisors.FirstOrDefaultAsync(x => x.UserId == user.Id);
                        profileData = new ProfileVm
                        {
                            Emails = new List<string>()
                            {
                                entity.Email
                            },
                            PhoneNumbers = new List<string>()
                            {
                                entity.Phone
                            },
                            Faculty = entity.Faculty.Title_EN,
                            FirstName = entity.Name,
                            LastName = entity.Surname
                        };
                        break;
                    }
                case Domain.Enums.Role.Administrator:
                    {
                        profileData = new ProfileVm
                        {
                            FirstName = user.Username
                        };
                        break;
                    }
                default:
                    {
                        throw new NotSupportedException($"Role with id {(int)user.Role} is not supported");
                    }
            }

            return profileData;
        }
    }
}
