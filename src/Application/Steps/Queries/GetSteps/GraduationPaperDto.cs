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
    }
}
