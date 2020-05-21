using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Supervisors.Commands.CreateSupervisor;

namespace tti_graduation_work.Application.IntegrationTests.TestData
{
    public class Supervisors
    {
        public static CreateSupervisorCommand Supervisor1(int facultyId, int jobTitleId, int userId, List<int> languages) =>
            new CreateSupervisorCommand
            {
                Degree = "Mg.sc.ing",
                Email = "test.sup@test.test",
                FacultyId = facultyId,
                JobPositionId = jobTitleId,
                Languages = languages,
                Name = "Test",
                Surname = "Supervisor 1",
                Phone = "+3733333333333",
                StaffId = 1,
                StudentLimit = 3,
                UserId = userId
            };

        public static CreateSupervisorCommand DuplicateSupervisor(int facultyId, int jobTitleId, int userId, List<int> languages)
            => Supervisor1(facultyId, jobTitleId, userId, languages);
    }
}
