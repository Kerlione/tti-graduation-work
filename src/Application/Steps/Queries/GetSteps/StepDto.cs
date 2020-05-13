using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Common.Mappings;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Steps.Queries.GetSteps
{
    public class StepDto: IMapFrom<Step>
    {
        public int Id { get; set; }
        public int StepType { get; set; }
        public int StepStatus { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Step, StepDto>()
                .ForMember(x => x.StepType, opt => opt.MapFrom(s => (int)s.StepType))
                .ForMember(x => x.StepStatus, opt => opt.MapFrom(s => (int)s.StepStatus));
        }
    }
}
