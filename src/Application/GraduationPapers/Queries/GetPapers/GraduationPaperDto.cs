using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Common.Mappings;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.GraduationPapers.Queries.GetPapers
{
    public class GraduationPaperDto : IMapFrom<GraduationPaper>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Supervisor { get; set; }
        public string Student { get; set; }
        public int PaperStatus { get; set; }
        public int PaperType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GraduationPaper, GraduationPaperDto>()
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title_EN))
                .ForMember(x => x.Supervisor, opt => opt.MapFrom(x => $"{x.Supervisor.Name} {x.Supervisor.Surname}"))
                .ForMember(x => x.Student, opt => opt.MapFrom(x => $"{x.Student.Name} {x.Student.Surname}"))
                .ForMember(x => x.PaperType, opt => opt.MapFrom(x => (int)x.PaperType))
                .ForMember(x => x.PaperStatus, opt => opt.MapFrom(x => (int)x.PaperStatus));
        }
    }
}
