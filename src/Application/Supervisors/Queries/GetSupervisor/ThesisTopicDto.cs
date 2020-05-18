using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Common.Mappings;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Supervisors.Queries.GetSupervisor
{
    public class ThesisTopicDto: IMapFrom<ThesisTopic>
    {
        public int Id { get; set; }
        public string Title_EN { get; set; }
        public string Title_RU { get; set; }
        public string Title_LV { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ThesisTopic, ThesisTopicDto>()
                .ForMember(f => f.Id, opt => opt.MapFrom(db => db.Id))
                .ForMember(f => f.Title_EN, opt => opt.MapFrom(db => db.Title_EN))
                .ForMember(f => f.Title_RU, opt => opt.MapFrom(db => db.Title_RU))
                .ForMember(f => f.Title_LV, opt => opt.MapFrom(db => db.Title_LV));
        }
    }
}
