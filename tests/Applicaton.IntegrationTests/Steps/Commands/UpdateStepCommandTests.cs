using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Exceptions;
using tti_graduation_work.Application.Faculties.Queries;
using tti_graduation_work.Application.GraduationPapers.Commands.CreatePaper;
using tti_graduation_work.Application.GraduationPapers.Queries.GetPaper;
using tti_graduation_work.Application.Steps.Commands;
using tti_graduation_work.Application.Steps.Commands.CreateSteps;
using tti_graduation_work.Application.Steps.Commands.FinishStep;
using tti_graduation_work.Application.Steps.Commands.RejectStep;
using tti_graduation_work.Application.Steps.Commands.SendStepToReviewRequest;
using tti_graduation_work.Application.Steps.Commands.UpdateStepCommand;
using tti_graduation_work.Application.Steps.Queries.GetStep;
using tti_graduation_work.Application.Steps.Queries.GetSteps;
using tti_graduation_work.Domain.Entities;
using tti_graduation_work.Domain.Enums;
using SingleStep = tti_graduation_work.Application.Steps.Queries.GetStep.StepDto;

namespace tti_graduation_work.Application.IntegrationTests.Steps.Commands
{
    using static Testing;
    public class UpdateStepCommandTests : TestBase
    {
        private int paperType = (int)PaperType.Bachelor;
        private int paperId = 0;
        private async Task<int> CreatePaper_withSteps(int studentId)
        {
            var paperCommand = new CreatePaperCommand
            {
                PaperType = paperType,
                StudentId = studentId
            };
            var paperId = await SendAsync(paperCommand);
            var command = new CreateStepsCommand
            {
                GraduationPaperId = paperId,
                PaperType = paperType
            };
            await SendAsync(command);
            return paperId;
        }

        private async Task<SingleStep> UpdateStep(int studentId, int supervisorId)
        {
            paperId = await CreatePaper_withSteps(studentId);
            var steps = await SendAsync(new GetStepsQuery { GraduationPaperId = paperId });
            var targetStep = steps.Steps.FirstOrDefault(x => x.StepType == (int)PaperStep.ThesisTopicApproval);
            var updateStepCommand = TestData.Steps.ThesisTopicStep(paperId, targetStep.Id, supervisorId, paperType);
            await SendAsync(updateStepCommand);
            return await SendAsync(new GetStepQuery { GraduationPaperId = paperId, StepId = targetStep.Id });
        }

        [Test]
        public async Task UpdateStepOfWork()
        {
            var student = await FirstOrDefault<Student>();
            paperId = await CreatePaper_withSteps(student.Id);
            var steps = await SendAsync(new GetStepsQuery { GraduationPaperId = paperId });
            var targetStep = steps.Steps.FirstOrDefault(x => x.StepType == (int)PaperStep.ThesisTopicApproval);
            var updateStepCommand = TestData.Steps.ThesisTopicStep(paperId, targetStep.Id, 0, paperType);
            await SendAsync(updateStepCommand);
            var updatedStep = await SendAsync(new GetStepQuery { GraduationPaperId = paperId, StepId = targetStep.Id });
            updatedStep.Should().NotBeNull();
            updatedStep.StepStatus.Should().Be((int)StepStatus.InProgress);
            updatedStep.Data.Should().NotBeEmpty();
        }

        [Test]
        public async Task SendStepToReview()
        {
            var student = await FirstOrDefault<Student>();
            var supervisor = await FirstOrDefault<Supervisor>();
            var step = await UpdateStep(student.Id, supervisor.Id);
            var command = new SendStepToReviewCommand { GraduationPaperId = paperId, StepId = step.Id };
            await SendAsync(command);
            var updatedStep = await SendAsync(new GetStepQuery { GraduationPaperId = paperId, StepId = step.Id });
            updatedStep.Should().NotBeNull();
            updatedStep.StepStatus.Should().Be((int)StepStatus.WaitingApproval);
            updatedStep.Data.Should().NotBeEmpty();
            var paper = await SendAsync(new GetPaperQuery
            {
                StudentId = student.Id
            });
            paper.Should().NotBeNull();
            paper.Supervisor.Should().Be($"{supervisor.Name} {supervisor.Surname}");
            paper.Title_EN.Should().Be("Title_EN");
            paper.Title_LV.Should().Be("Title_LV");
            paper.Title_RU.Should().Be("Title_RU");
        }

        [Test]
        public async Task SendStepToReview_SupervisorNotAttached()
        {
            var student = await FirstOrDefault<Student>();
            var step = await UpdateStep(student.Id, 0);
            var command = new SendStepToReviewCommand { GraduationPaperId = paperId, StepId = step.Id };
            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<NotFoundException>();
        }


        private async Task<int> CreateSecondStudent()
        {
            var faculties = await SendAsync(new GetFacultiesQuery());
            var faculty = faculties.Faculties.FirstOrDefault();
            var programe = faculty.Programes.FirstOrDefault();
            // Create student user
            var studentUserId = await SendAsync(TestData.Users.Student2User);
            // Create fake student
            return await SendAsync(TestData.Students.Student2(faculty.Id, programe.Id, studentUserId));
        }

        private async Task<StepsVm> GetSteps(int paperId)
        {
            return await SendAsync(new GetStepsQuery { GraduationPaperId = paperId });
        }

        private async Task<SingleStep> GetStep(int paperId, int stepId)
        {
            return await SendAsync(new GetStepQuery { GraduationPaperId = paperId, StepId = stepId });
        }

        [Test]
        public async Task UpdateStep_NotAttachedToWork()
        {
            var student1 = await FirstOrDefault<Student>();
            var paperId_Student1 = await CreatePaper_withSteps(student1.Id);
            var student2 = await CreateSecondStudent();
            var paperId_Student2 = await CreatePaper_withSteps(student2);
            var steps = await GetSteps(paperId_Student2);
            var command = new UpdateStepCommand
            {
                GraduationPaperId = paperId_Student1,
                Data = "{}",
                StepId = steps.Steps.FirstOrDefault().Id
            };
            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<NotAccessibleEntityException>();
        }

        [Test]
        public async Task ApproveStep()
        {
            var supervisor = await FirstOrDefault<Supervisor>();
            var student = await FirstOrDefault<Student>();
            var step = await UpdateStep(student.Id, supervisor.Id);
            var command = new SendStepToReviewCommand { GraduationPaperId = paperId, StepId = step.Id };
            await SendAsync(command);
            var approveCommand = new ApproveStepCommand { GraduationPaperId = paperId, StepId = step.Id, SupervisorId = supervisor.Id };
            await SendAsync(approveCommand);
            var steps = await GetSteps(paperId);
            var targetStep = steps.Steps.FirstOrDefault(x => x.Id == step.Id);
            targetStep.StepStatus.Should().Be((int)StepStatus.Approved);
        }

        [Test]
        public async Task RejectStep()
        {
            var supervisor = await FirstOrDefault<Supervisor>();
            var student = await FirstOrDefault<Student>();
            var step = await UpdateStep(student.Id, supervisor.Id);
            var command = new SendStepToReviewCommand { GraduationPaperId = paperId, StepId = step.Id };
            await SendAsync(command);
            var rejectCommand = new RejectStepCommand { GraduationPaperId = paperId, StepId = step.Id, Reason = "Test" };
            await SendAsync(rejectCommand);
            var targetStep = await SendAsync( new GetStepQuery { StepId = step.Id, GraduationPaperId = paperId });
            targetStep.StepStatus.Should().Be((int)StepStatus.Rejected);
            targetStep.Comment.Should().Be("Test");
        }

        [Test]
        public async Task FinishStep()
        {
            var supervisor = await FirstOrDefault<Supervisor>();
            var student = await FirstOrDefault<Student>();
            var step = await UpdateStep(student.Id, supervisor.Id);
            var command = new SendStepToReviewCommand { GraduationPaperId = paperId, StepId = step.Id };
            await SendAsync(command);
            var approveCommand = new ApproveStepCommand { GraduationPaperId = paperId, StepId = step.Id, SupervisorId = supervisor.Id };
            await SendAsync(approveCommand);
            step = await GetStep(paperId, step.Id);
            step.StepStatus.Should().Be((int)StepStatus.Approved);
            var finishCommand = new FinishStepCommand { GraduationPaperId = paperId, StepId = step.Id };
            await SendAsync(finishCommand);
            step = await GetStep(paperId, step.Id);
            step.StepStatus.Should().Be((int)StepStatus.Finished);
        }

    }
}
