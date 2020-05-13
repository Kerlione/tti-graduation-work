using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Common;
using tti_graduation_work.Domain.Enums;

namespace tti_graduation_work.Domain.Entities
{
    public class GraduationPaper: MultilangualTitle
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int SupervisorId { get; set; }
        public Supervisor Supervisor { get; set; }
        public int Year { get; set; }
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public PaperStatus PaperStatus { get; set; }
        public PaperType PaperType { get; set; }
        public ICollection<Step> Steps { get; set; }
    }
}
