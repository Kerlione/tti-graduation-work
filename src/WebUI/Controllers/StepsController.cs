using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tti_graduation_work.Application.Steps.Commands;
using tti_graduation_work.Application.Steps.Commands.FinishStep;
using tti_graduation_work.Application.Steps.Commands.NotifyStudent;
using tti_graduation_work.Application.Steps.Commands.NotifySupervisor;
using tti_graduation_work.Application.Steps.Commands.RejectStep;
using tti_graduation_work.Application.Steps.Commands.SendStepToReviewRequest;
using tti_graduation_work.Application.Steps.Commands.UpdateStepCommand;
using tti_graduation_work.Application.Steps.Commands.UploadAttachment;
using tti_graduation_work.Application.Steps.Queries.GetAvailableSupervisors;
using tti_graduation_work.Application.Steps.Queries.GetStep;
using tti_graduation_work.Application.Steps.Queries.GetSteps;
using SingleStep = tti_graduation_work.Application.Steps.Queries.GetStep.StepDto;

namespace tti_graduation_work.WebUI.Controllers
{
    //[Authorize]
    public class StepsController : ApiController
    {
        [HttpPost("NotifyStudent/{id}")]
        public async Task<ActionResult> NotifyStudent(int id, NotifyStudentCommand request)
        {
            if (id != request.StepId)
            {
                return BadRequest();
            }
            await Mediator.Send(request);
            return NoContent();
        }

        [HttpPost("NotifySupervisor/{id}")]
        public async Task<ActionResult> NotifySupervisor(int id, NotifySupervisorCommand request)
        {
            if (id != request.StepId)
            {
                return BadRequest();
            }
            await Mediator.Send(request);
            return NoContent();
        }

        [HttpGet("{id}/Steps")]
        public async Task<ActionResult<StepsVm>> GetSteps(int id)
        {
            return await Mediator.Send(new GetStepsQuery { GraduationPaperId = id });
        }

        [HttpPost("{id}/Step/{stepId}")]
        public async Task<ActionResult<SingleStep>> GetStep(int id, int stepId)
        {
            return await Mediator.Send(new GetStepQuery { GraduationPaperId = id, StepId = stepId });
        }

        [HttpPost("{id}/Step/{stepId}/Attachment")]
        public async Task<ActionResult<int>> UploadAttachment(int id, int stepId, IFormFile file)
        {
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = Convert.ToBase64String(ms.ToArray());
                    await Mediator.Send(new UploadAttachmentCommand
                    {
                        GraduationPaperId = id,
                        StepId = stepId,
                        Name = file.FileName,
                        Data = Convert.FromBase64String(fileBytes)
                    });
                }
            }
            return Ok();
        }

        [HttpPut("Step/{stepId}/Update")]
        public async Task<ActionResult> UpdateStep(int stepId, UpdateStepCommand request)
        {
            if (stepId != request.StepId)
            {
                return BadRequest();
            }

            await Mediator.Send(request);

            return Ok();
        }

        [HttpPost("{id}/Step/{stepId}/Approve")]
        public async Task<ActionResult> ApproveStep(int id, int stepId, ApproveStepCommand request)
        {
            if (id != request.GraduationPaperId)
            {
                return BadRequest();
            }

            if (stepId != request.StepId)
            {
                return BadRequest();
            }

            await Mediator.Send(request);

            return Ok();
        }

        [HttpPost("{id}/Step/{stepId}/Reject")]
        public async Task<ActionResult> RejectStep(int id, int stepId, RejectStepCommand request)
        {
            if (id != request.GraduationPaperId)
            {
                return BadRequest();
            }

            if (stepId != request.StepId)
            {
                return BadRequest();
            }

            await Mediator.Send(request);

            return Ok();
        }

        [HttpPost("{id}/Step/{stepId}/Finish")]
        public async Task<ActionResult> FinishStep(int id, int stepId, FinishStepCommand request)
        {
            if (id != request.GraduationPaperId)
            {
                return BadRequest();
            }

            if (stepId != request.StepId)
            {
                return BadRequest();
            }

            await Mediator.Send(request);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<SupervisorsVm>> GetAvailableSupervisors()
        {
            return await Mediator.Send(new GetAvailableSupervisorsQuery());
        }

        [HttpPost("{id}/Step/{stepId}/ToReview")]
        public async Task<ActionResult> SendToReview(int id, int stepId, SendStepToReviewCommand request)
        {
            if (request.GraduationPaperId != id)
            {
                return BadRequest();
            }
            if (request.StepId != stepId)
            {
                return BadRequest();
            }
            await Mediator.Send(request);

            return Ok();
        }
    }
}
