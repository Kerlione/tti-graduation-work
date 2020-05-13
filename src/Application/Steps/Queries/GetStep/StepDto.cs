using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Common.Mappings;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Steps.Queries.GetStep
{
    public class StepDto: IMapFrom<Step>
    {
        public StepDto()
        {
            Attachments = new List<AttachmentDto>();
        }
        public int Id { get; set; }
        public int StepType { get; set; }
        public string Data { get; set; }
        public IList<AttachmentDto> Attachments { get; set; }
    }
}
