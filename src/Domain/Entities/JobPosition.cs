using System.Collections.Generic;
using tti_graduation_work.Domain.Common;

namespace tti_graduation_work.Domain.Entities
{
    public class JobPosition: MultilangualTitle
    {
        public int Id { get; set; }
        public ICollection<Supervisor> Supervisors { get; set; }
    }
}