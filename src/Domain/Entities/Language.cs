using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Common;

namespace tti_graduation_work.Domain.Entities
{
    public class Language: MultilangualTitle
    {
        public int Id { get; set; }
        public ICollection<SupervisorLanguage> SupervisorLanguages { get; set; }
    }
}
