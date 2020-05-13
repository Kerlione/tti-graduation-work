using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Application.Steps.Queries.GetSteps
{
    public class StepsVm
    {
        public IList<StepStatusDto> Statuses { get; set; }
        public IList<StepTypeDto> Types { get; set; }
        public IList<StepDto> Steps { get; set; }
        public GraduationPaperDto GradautionPaper { get; set; }
    }
}
