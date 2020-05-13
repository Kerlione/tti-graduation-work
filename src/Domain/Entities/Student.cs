using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int ProgrameId { get; set; }
        public Programe Programe { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int GranduationPaperId { get; set; }
        public GraduationPaper GraduationPaper { get; set; }
        public Language Language { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Skype { get; set; }
        public string Comment { get; set; }
    }
}
