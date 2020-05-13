using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tti_graduation_work.Application.Common.Mappings;
using tti_graduation_work.Domain.Entities;
using tti_graduation_work.Domain.Enums;

namespace tti_graduation_work.Application.Students.Queries.GetStudents
{
    public class StudentDto : IMapFrom<Student>
    {
        public StudentDto()
        {

        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int FinishedStepCount { get; set; }
        public int TotalStepCount { get; set; }
        public string Faculty { get; set; }
        public string Programe { get; set; }
        public string StudyLanguage { get; set; }
        public string Degree { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Student, StudentDto>()
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Surname))
                //.ForMember(d => d.FinishedStepCount, opt => opt.MapFrom(s => s.GraduationPaper.Steps
                //    .Where(x => x.StepStatus.Equals(StepStatus.Finished))))
                //.ForMember(d => d.TotalStepCount, opt => opt.MapFrom(s => s.GraduationPaper.Steps.Count()))
                .ForMember(d => d.Faculty, opt => opt.MapFrom(s => s.Programe.Faculty.Title_EN))
                .ForMember(d => d.Programe, opt => opt.MapFrom(s => s.Programe.Title_EN))
                .ForMember(d => d.StudyLanguage, opt => opt.MapFrom(s => s.Language.Title_EN));
        }
    }
}
