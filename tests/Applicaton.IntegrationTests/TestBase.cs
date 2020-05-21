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
        [OneTimeSetUp]
        public async Task SeedData()
        {
            // Seed database
            await SendAsync(new SyncQuery());
            var students = GetAll<Student>();
            if (!students.Any())
            {
                await AddStudent();
            }
            var supervisors = GetAll<Supervisor>();
            if (!supervisors.Any())
            {
                await AddSupervisor();
            }
        }

        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }

        private async Task AddSupervisor()
        {
            var faculty = GetAll<Faculty>().FirstOrDefault();
            // Create supervisor user
            var supervisorUserId = await SendAsync(TestData.Users.SupervisorUser);
            // Create fake supervisor
            await SendAsync(TestData.Supervisors.Supervisor1(faculty.Id, 1, supervisorUserId, new List<int> { 1, 2, 3 }));
        }
        private async Task AddStudent()
        {
            var faculty = GetAll<Faculty>().FirstOrDefault();
            var programe = GetAll<Programe>().FirstOrDefault(x => x.FacultyId == faculty.Id);
            // Create student user
            var studentUserId = await SendAsync(TestData.Users.StudentUser);
            // Create fake student
            await SendAsync(TestData.Students.Student1(faculty.Id, programe.Id, studentUserId));
        }
    }
}
