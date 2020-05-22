using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Common.Mappings;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Steps.Queries.GetStep
{
    public class StepDto : IMapFrom<Step>
    {
        public StepDto()
        {
            Attachments = new List<AttachmentDto>();
        }
        public int Id { get; set; }
        public int StepType { get; set; }
        public int StepStatus { get; set; }
        public string Data { get; set; }
        public IList<AttachmentDto> Attachments { get; set; }
        public string Comment { get; set; }
        public int StudentId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Step, StepDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.StepStatus, opt => opt.MapFrom(s => s.StepStatus))
                .ForMember(x => x.StepType, opt => opt.MapFrom(s => s.StepType))
                .ForMember(x => x.Data, opt => opt.MapFrom(s => s.StepData))
                .ForMember(x => x.Comment, opt => opt.MapFrom(s => s.Comment));
        }
    }
}
