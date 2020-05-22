using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tti_graduation_work.Application.GraduationPapers.Commands.CreatePaper;
using tti_graduation_work.Application.Steps.Commands.CreateSteps;
using tti_graduation_work.Application.Steps.Queries.GetSteps;
using tti_graduation_work.Domain.Entities;
using tti_graduation_work.Domain.Enums;

namespace tti_graduation_work.Application.IntegrationTests.Steps.Commands
{
    using static Testing;
    public class CreateStepsCommandTests : TestBase
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

        private int StepCount(PaperType paperType)
        {
            return paperType == PaperType.Bachelor ? 9 : 11;
        }

        [Test]
        public async Task CreateSteps()
        {
            var paperId = await CreatePaper();
            var paperType = PaperType.Bachelor;
            var command = new CreateStepsCommand
            {
                GraduationPaperId = paperId,
                PaperType = (int)paperType
            };

            await SendAsync(command);

            var steps = await SendAsync(new GetStepsQuery { GraduationPaperId = paperId });
            steps.Should().NotBeNull();
            steps.GraduationPaper.Should().NotBeNull();
            steps.Steps.Should().NotBeEmpty();
            steps.Steps.Count.Should().Be(StepCount(paperType));
        }

        [Test]
        public async Task CheckBachelorStepCount()
        {
            var paperId = await CreatePaper();
            var paperType = PaperType.Bachelor;
            var command = new CreateStepsCommand
            {
                GraduationPaperId = paperId,
                PaperType = (int)paperType
            };

            await SendAsync(command);

            var steps = await SendAsync(new GetStepsQuery { GraduationPaperId = paperId });
            steps.Should().NotBeNull();
            steps.GraduationPaper.Should().NotBeNull();
            steps.Steps.Should().NotBeEmpty();
            steps.Steps.Count.Should().Be(StepCount(paperType));
        }

        [Test]
        public async Task CheckMasterStepCount()
        {
            var paperId = await CreatePaper();
            var paperType = PaperType.Master;
            var command = new CreateStepsCommand
            {
                GraduationPaperId = paperId,
                PaperType = (int)paperType
            };

            await SendAsync(command);

            var steps = await SendAsync(new GetStepsQuery { GraduationPaperId = paperId });
            steps.Should().NotBeNull();
            steps.GraduationPaper.Should().NotBeNull();
            steps.Steps.Should().NotBeEmpty();
            steps.Steps.Count.Should().Be(StepCount(paperType));
        }
    }
}
