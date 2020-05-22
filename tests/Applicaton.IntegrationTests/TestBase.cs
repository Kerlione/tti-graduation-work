using NUnit.Framework;
using System.Threading.Tasks;
using tti_graduation_work.Application.Users.Queries.Sync;
using tti_graduation_work.Application.IntegrationTests.TestData;
using tti_graduation_work.Application.Faculties.Queries;
using System.Linq;
using System.Collections.Generic;
using tti_graduation_work.Application.Students.Queries.GetStudents;
using tti_graduation_work.Application.Supervisors.Queries.GetSupervisors;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.IntegrationTests
{
    using static Testing;

    public class TestBase
    {
        public async Task SeedData()
        {
            // Seed database
            await SendAsync(new SyncQuery());
            var students = await SendAsync(new GetStudentsQuery { Skip = 0, Take = 15 });
            if (students.Total == 0)
            {
                await AddStudent();
            }
            var supervisors = await SendAsync(new GetSupervisorsQuery { Skip = 0, Take = 15 });
            if (supervisors.Total == 0)
            {
                await AddSupervisor();
            }
        }

        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
            await SeedData();
        }

        private async Task AddSupervisor()
        {
            var jobPosition = await FirstOrDefault<JobPosition>();
            var language = await FirstOrDefault<Language>();
            var faculties = await SendAsync(new GetFacultiesQuery());
            var faculty = faculties.Faculties.FirstOrDefault();
            // Create supervisor user
            var supervisorUserId = await SendAsync(TestData.Users.SupervisorUser);
            // Create fake supervisor
            await SendAsync(TestData.Supervisors.Supervisor1(faculty.Id, jobPosition.Id, supervisorUserId, new List<int> { language.Id }));
        }
        private async Task AddStudent()
        {
            var faculties = await SendAsync(new GetFacultiesQuery());
            var faculty = faculties.Faculties.FirstOrDefault();
            var programe = faculty.Programes.FirstOrDefault();
            // Create student user
            var studentUserId = await SendAsync(TestData.Users.Student1User);
            // Create fake student
            await SendAsync(TestData.Students.Student1(faculty.Id, programe.Id, studentUserId));
        }

        [TearDown]
        public async Task Finish()
        {

        }
    }
}
