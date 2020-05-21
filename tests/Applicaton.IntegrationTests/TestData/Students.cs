using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Students.Commands;

namespace tti_graduation_work.Application.IntegrationTests.TestData
{
    public class Students
    {
        public static CreateStudentCommand Student1(int facultyId, int programeId, int userId) => new CreateStudentCommand
        {
            ExternalId = 1,
            Comments = "",
            Emails = new List<string>
            {
                "testmail1@test.test",
                "testmail2@test.test"
            },
            FacultyId = facultyId,
            UserId = userId,
            Form = 1,
            Language = "RU",
            Name = "Test",
            Surname = "Student 1",
            Phones = new List<string>
            {
                "+311111111111111",
                "+322222222222222"
            },
            ProgrameId = programeId,
            Skype = "test1",
            Status = "Active"
        };
    }
}
