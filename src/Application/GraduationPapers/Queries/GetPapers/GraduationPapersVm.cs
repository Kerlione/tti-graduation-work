using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Application.GraduationPapers.Queries.GetPapers
{
    public class GraduationPapersVm
    {
        public IList<PaperStatusDto> PaperStatuses { get; set; }
        public IList<PaperTypeDto> PaperTypes { get; set; }
        public IList<GraduationPaperDto> GraduationPapers { get; set; }
        public int Total { get; set; }
    }
}
