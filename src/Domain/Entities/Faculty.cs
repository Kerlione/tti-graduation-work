using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Common;

namespace tti_graduation_work.Domain.Entities
{
    public class Faculty: MultilangualTitle
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string ShortTitle_EN { get; set; }
        public string ShortTitle_LV { get; set; }
        public string ShortTitle_RU { get; set; }

        public ICollection<Programe> Programes { get; set; }

    }
}
