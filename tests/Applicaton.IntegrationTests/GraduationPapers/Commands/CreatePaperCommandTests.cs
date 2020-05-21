using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tti_graduation_work.Application.Students.Commands;
using tti_graduation_work.Application.Users.Commands.CreateUser;
using tti_graduation_work.Application.Users.Queries.GetUsers;
using tti_graduation_work.Application.Users.Queries.Sync;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.IntegrationTests.GraduationPapers.Commands
{
    using static Testing;
    public class CreatePaperCommandTests: TestBase
    {
        [Test]
         public async Task ShouldCreatePaper()
        {
            var student = await FindAsync<Student>(1);
        }
    }
}
