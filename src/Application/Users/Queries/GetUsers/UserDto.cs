using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Common.Mappings;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Users.Queries.GetUsers
{
    public class UserDto : IMapFrom<User>
    {
        public string Username { get; set; }
        public int Status { get; set; }
        public int Role { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>()
                .ForMember(u => u.Status, opt => opt.MapFrom(s => (int)s.Status))
                .ForMember(u => u.Role, opt => opt.MapFrom(r => (int)r.Role));
        }
    }
}
