using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Domain.Entities
{
    public class SupervisorLanguage
    {
        public int SupervisorId { get; set; }
        public Supervisor Supervisor { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
