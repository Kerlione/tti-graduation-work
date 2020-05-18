using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Common.Mappings;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Steps.Queries.GetSteps
{
    public class GraduationPaperDto: IMapFrom<GraduationPaper>
    {
        public string Title { get; set; }
        public string Student { get; set; }
        public string Supervisor { get; set; }
        public int SupervisorId { get; set; }
        public int Year { get; set; }
        public string PaperType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GraduationPaper, GraduationPaperDto>()
                .ForMember(p => p.Title, opt => opt.MapFrom(s => String.IsNullOrEmpty(s.Title_EN) ? "No topic" : s.Title_EN))
                .ForMember(p => p.Student, opt => opt.MapFrom(s => $"{s.Student.Name} {s.Student.Surname}"))
                .ForMember(p => p.Supervisor, opt => opt.MapFrom(s => s.SupervisorId.HasValue ? $"{s.Supervisor.Name} {s.Supervisor.Surname}" : "No supervisor"))
                .ForMember(p => p.SupervisorId, opt => opt.MapFrom(s => s.SupervisorId))
                .ForMember(p => p.PaperType, opt => opt.MapFrom(s => s.PaperType.ToString()))
                .ForMember(p => p.Year, opt => opt.MapFrom(s => s.Year));                
        }
    }
}
