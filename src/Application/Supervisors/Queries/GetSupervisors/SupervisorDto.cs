using AutoMapper;
using tti_graduation_work.Application.Common.Mappings;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Supervisors.Queries.GetSupervisors
{
    public class SupervisorDto : IMapFrom<Supervisor>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Faculty { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Supervisor, SupervisorDto>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(s => s.Name))
                .ForMember(x => x.LastName, opt => opt.MapFrom(s => s.Surname))
                .ForMember(x => x.Faculty, opt => opt.MapFrom(s => s.Faculty.Title_EN));
        }
    }
}