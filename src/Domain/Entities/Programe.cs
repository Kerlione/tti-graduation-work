using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Common;

namespace tti_graduation_work.Domain.Entities
{
    public class Programe: MultilangualTitle
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
    }
}
