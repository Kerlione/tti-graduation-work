using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using tti_graduation_work.Application.Common.Mappings;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Supervisors.Queries.GetSupervisor
{
    public class SupervisorDto : IMapFrom<Supervisor>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<string> Languages { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IList<ThesisTopicDto> Topics { get; set; }
        public IList<FieldOfInterestDto> FieldsOfInterest { get; set; }
        public string Faculty { get; set; }
        public string JobTitle { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Supervisor, SupervisorDto>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(s => s.Name))
                .ForMember(x => x.LastName, opt => opt.MapFrom(s => s.Surname))
                .ForMember(x => x.Languages, opt => opt.MapFrom(s => s.SupervisorLanguages.Select(sl => sl.Language.Title_EN)))
                .ForMember(x => x.Topics, opt => opt.MapFrom(s => s.ThesisTopics.Select(t => t.Title_EN)))
                .ForMember(x => x.FieldsOfInterest, opt => opt.MapFrom(s => s.FieldsOfInterest.Select(f => f.Title_EN)))
                .ForMember(x => x.Faculty, opt => opt.MapFrom(s => s.Faculty.Title_EN))
                .ForMember(x => x.JobTitle, opt => opt.MapFrom(s => s.JobPosition.Title_EN))
                .ForMember(x => x.FieldsOfInterest, opt => opt.MapFrom(s => s.FieldsOfInterest))
                .ForMember(x => x.Topics, opt => opt.MapFrom(s => s.ThesisTopics));
        }
    }
}