using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Enums;

namespace tti_graduation_work.Domain.Entities
{
    public class Step
    {
        public int Id { get; set; }
        public StepType StepType { get; set; }
        public string StepData { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        public StepStatus StepStatus { get; set; }
        public string Comment { get; set; }
        public int GraduationPaperId { get; set; }
        public GraduationPaper GraduationPaper { get; set; }
    }
}
