using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Steps.Commands.UpdateStepCommand;
using tti_graduation_work.Domain.Entities;
using tti_graduation_work.Domain.Enums;

namespace tti_graduation_work.Application.IntegrationTests.TestData
{
    public class Steps
    {
        public static UpdateStepCommand ThesisTopicStep(int paperId, int stepId, int supervisorId, int paperType) =>
            new UpdateStepCommand
            {
                GraduationPaperId = paperId,
                StepId = stepId,
                Data = $"{{\"title_LV\":\"Title_LV\",\"title_EN\":\"Title_EN\",\"title_RU\":\"Title_RU\",\"supervisorId\":{supervisorId},\"paperType\":{paperType}}}"
            };

        public static Step Step(int paperId, int stepId, int supervisorId, int paperType) =>
            new Step
            {
                GraduationPaperId = paperId,
                Id = stepId,
                StepData = $"{{\"title_LV\":\"Title_LV\",\"title_EN\":\"Title_EN\",\"title_RU\":\"Title_RU\",\"supervisorId\":{supervisorId},\"paperType\":{paperType}}}",
                StepType = PaperStep.ThesisTopicApproval
            };

        public static GraduationPaper Paper(int id, int studentId, int supervisorId, PaperType paperType, int facultyId) =>
            new GraduationPaper
            {
                SupervisorId = supervisorId,
                StudentId = studentId,
                Title_EN = "Title_EN",
                Title_LV = "Title_LV",
                Title_RU = "Title_RU",
                PaperType = paperType,
                PaperStatus = PaperStatus.InProgress,
                Year = DateTime.UtcNow.Year,
                FacultyId = facultyId,
                Id = id
            };
    }
}
