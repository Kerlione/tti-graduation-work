using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Common;

namespace tti_graduation_work.Domain.Entities
{
    public class FieldOfInterest: MultilangualTitle
    {
        public int Id { get; set; }
        public int SupervisorId { get; set; }
        public Supervisor Supervisor { get; set; }
    }
}
