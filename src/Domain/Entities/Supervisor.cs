using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Domain.Entities
{
    public class Supervisor
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public int JobPositionId { get; set; }
        public JobPosition JobPosition { get; set; }
        public ICollection<GraduationPaper> GraduationPapers { get; set; }
        public ICollection<SupervisorLanguage> SupervisorLanguages { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Degree { get; set; }
        public ICollection<ThesisTopic> ThesisTopics { get; set; }
        public ICollection<FieldOfInterest> FieldsOfInterest { get; set; }
        public int StudentLimit { get; set; }
    }
}
