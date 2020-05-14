using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tti_graduation_work.Application.Steps.Commands;
using tti_graduation_work.Application.Steps.Commands.FinishStep;
using tti_graduation_work.Application.Steps.Commands.NotifyStudent;
using tti_graduation_work.Application.Steps.Commands.NotifySupervisor;
using tti_graduation_work.Application.Steps.Commands.RejectStep;
using tti_graduation_work.Application.Steps.Commands.UpdateStepCommand;
using tti_graduation_work.Application.Steps.Commands.UploadAttachment;
using tti_graduation_work.Application.Steps.Queries.GetStep;
using tti_graduation_work.Application.Steps.Queries.GetSteps;
using SingleStep = tti_graduation_work.Application.Steps.Queries.GetStep.StepDto;

namespace tti_graduation_work.WebUI.Controllers
{
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
        public async Task<ActionResult<SingleStep>> GetStep(int id, int stepId, GetStepQuery request)
        {
            if(id != request.GraduationPaperId)
            {
                return BadRequest();
            }

            if(stepId != request.StepId)
            {
                return BadRequest();
            }

            return await Mediator.Send(request);
        }

        [HttpPut("{id}/Step/{stepId}/Attachment")]
        public async Task<ActionResult<int>> UploadAttachment(int id, int stepId, UploadAttachmentCommand request)
        {
            if (id != request.GraduationPaperId)
            {
                return BadRequest();
            }

            if (stepId != request.StepId)
            {
                return BadRequest();
            }

            return await Mediator.Send(request);
        }

        [HttpPost("{id}/Step/{stepId}/Update")]
        public async Task<ActionResult> UpdateStep(int id, int stepId, UpdateStepCommand request)
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
    }
}
