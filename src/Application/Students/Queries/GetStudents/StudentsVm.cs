using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Application.Students.Queries.GetStudents
{
    public class StudentsVm
    {
        public IList<StudentDto> Students { get; set; }
        public int Total { get; set; }
    }
}
