using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Exceptions;
using tti_graduation_work.Application.GraduationPapers.Commands.CreatePaper;
using tti_graduation_work.Application.Steps.Queries.GetSteps;
using tti_graduation_work.Domain.Entities;
using tti_graduation_work.Domain.Enums;

namespace tti_graduation_work.Application.IntegrationTests.Steps.Queries
{
    using static Testing;

    public class GetStepsQueryTests: TestBase
    {
        private async Task<int> CreatePaper()
        {
            var student = await FirstOrDefault<Student>();
            var command = new CreatePaperCommand
            {
                PaperType = (int)PaperType.Bachelor,
                StudentId = student.Id
            };
            return await SendAsync(command);
        }

        [Test]
        public async Task GetSteps()
        {
            var paperId = await CreatePaper();

            var steps = await SendAsync(new GetStepsQuery { GraduationPaperId = paperId });

            steps.Should().NotBeNull();
            steps.GraduationPaper.Should().NotBeNull();
            steps.Steps.Should().BeEmpty();
        }

        [Test]
        public async Task GetSteps_NotExistingPaper()
        {
            var paperId = 0;
            var command = new GetStepsQuery { GraduationPaperId = paperId };
            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<NotFoundException>();
        }
    }
}
