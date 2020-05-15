using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Common.Mappings;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Steps.Queries.GetAvailableSupervisors
{
    public class SupervisorSm: IMapFrom<Supervisor>
    {
        public int Key { get; set; }
        public string Value { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Supervisor, SupervisorSm>()
                .ForMember(s => s.Key, opt => opt.MapFrom(db => db.Id))
                .ForMember(s => s.Value, opt => opt.MapFrom(db => $"{db.Name} {db.Surname}"));
        }
    }
}
