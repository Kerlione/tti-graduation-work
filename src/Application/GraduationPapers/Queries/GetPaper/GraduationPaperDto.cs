using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Common.Mappings;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.GraduationPapers.Queries.GetPaper
{
    public class GraduationPaperDto: IMapFrom<GraduationPaper>
    {
        public int Id { get; set; }
        public string Title_RU { get; set; }
        public string Title_EN { get; set; }
        public string Title_LV { get; set; }
        public string Supervisor { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GraduationPaper, GraduationPaperDto>()
                .ForMember(dto => dto.Supervisor, opt => opt.MapFrom(p => p.SupervisorId != 0 ? $"{p.Supervisor.Name} {p.Supervisor.Surname}": "None"));
        }
    }
}
