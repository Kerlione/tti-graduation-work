using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tti_graduation_work.Application.GraduationPapers.Commands.CreatePaper;
using tti_graduation_work.Application.GraduationPapers.Queries.GetPaper;
using tti_graduation_work.Application.Students.Commands;
using tti_graduation_work.Application.Users.Commands.CreateUser;
using tti_graduation_work.Application.Users.Queries.GetUsers;
using tti_graduation_work.Application.Users.Queries.Sync;
using tti_graduation_work.Domain.Entities;
using tti_graduation_work.Domain.Enums;

namespace tti_graduation_work.Application.IntegrationTests.GraduationPapers.Commands
{
    using static Testing;
    public class CreatePaperCommandTests : TestBase
    {
        [Test]
        public async Task CheckNoPaperByDefault()
        {
            var student = await FirstOrDefault<Student>();
            var paper = await SendAsync(new GetPaperQuery { StudentId = student.Id });
            paper.Should().BeNull();
        }

        [Test]
        public async Task CreatePaper()
        {
            var student = await FirstOrDefault<Student>();
            var paper = await SendAsync(new GetPaperQuery { StudentId = student.Id });
            paper.Should().BeNull();

            var command = new CreatePaperCommand
            {
                PaperType = (int)PaperType.Bachelor,
                StudentId = student.Id
            };

            var paperId = await SendAsync(command);
            paperId.Should().NotBe(0);

            paper = await SendAsync(new GetPaperQuery { StudentId = student.Id });
            paper.Should().NotBeNull();
            paper.Supervisor.Should().Be("None");
            paper.Title_EN.Should().BeNull();
            paper.Title_LV.Should().BeNull();
            paper.Title_RU.Should().BeNull();
        }
    }
}
