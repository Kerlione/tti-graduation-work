using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Common.Mappings;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Users.Queries.GetProfile
{
    public class ProfileVm: IMapFrom<Student>, IMapFrom<Supervisor>, IMapFrom<User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<string> Emails { get; set; }
        public IList<string> PhoneNumbers { get; set; }
        public string Faculty { get; set; }
        public string Programe { get; set; }
        public string Role { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Student, ProfileVm>()
                .ForMember(p => p.FirstName, opt => opt.MapFrom(s => s.Name))
                .ForMember(p => p.LastName, opt => opt.MapFrom(s => s.Surname))
                .ForMember(p => p.Faculty, opt => opt.MapFrom(s => s.Programe.Faculty.Title_EN))
                .ForMember(p => p.Programe, opt => opt.MapFrom(s => s.Programe.Title_EN))
                .ForMember(p => p.Emails, opt => opt.MapFrom(s => new List<string> { s.Email1, s.Email2 }))
                .ForMember(p => p.PhoneNumbers, opt => opt.MapFrom(s => new List<string> { s.Phone1, s.Phone2 }))
                .ForMember(p => p.Role, opt => opt.MapFrom(s => s.User.Role.ToString()));
            profile.CreateMap<Supervisor, ProfileVm>()
                .ForMember(p => p.FirstName, opt => opt.MapFrom(s => s.Name))
                .ForMember(p => p.LastName, opt => opt.MapFrom(s => s.Surname))
                .ForMember(p => p.Faculty, opt => opt.MapFrom(s => s.Faculty.Title_EN))
                .ForMember(p => p.Emails, opt => opt.MapFrom(s => new List<string> { s.Email }))
                .ForMember(p => p.PhoneNumbers, opt => opt.MapFrom(s => new List<string> { s.Phone }))
                .ForMember(p => p.Role, opt => opt.MapFrom(s => s.User.Role.ToString()));
            profile.CreateMap<User, ProfileVm>()
                .ForMember(p => p.FirstName, opt => opt.MapFrom(s => s.Username))
                .ForMember(p => p.Role, opt => opt.MapFrom(s => s.Role.ToString()));
        }
    }
}
