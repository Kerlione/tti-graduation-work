﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tti_graduation_work.Application.Fields.Commands.CreateField;
using tti_graduation_work.Application.Fields.Commands.DeleteField;
using tti_graduation_work.Application.Fields.Commands.UpdateField;
using tti_graduation_work.Application.Supervisors.Commands.CreateSupervisor;
using tti_graduation_work.Application.Supervisors.Commands.UpdateStudentCount;
using tti_graduation_work.Application.Supervisors.Queries.GetSupervisor;
using tti_graduation_work.Application.Supervisors.Queries.GetSupervisors;
using tti_graduation_work.Application.Topics.Commands.CreateTopic;
using tti_graduation_work.Application.Topics.Commands.DeleteTopic;
using tti_graduation_work.Application.Topics.Commands.UpdateTopic;
using tti_graduation_work.Application.Users.Commands.CreateUser;
using tti_graduation_work.Application.Users.Queries.GetUser;
using tti_graduation_work.WebUI.Attributes;
using tti_graduation_work.WebUI.Enums;
using SingleSupervisor = tti_graduation_work.Application.Supervisors.Queries.GetSupervisor.SupervisorDto;

namespace tti_graduation_work.WebUI.Controllers
{
    [Authorize]
    public class SupervisorsController : ApiController
    {
        [HttpPost("{id}/Profile")]
        public async Task<ActionResult<SingleSupervisor>> GetProfile(int id, GetSupervisorQuery request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            return await Mediator.Send(request);
        }

        [HttpPut("Create")]
        public async Task<ActionResult<int>> Create(CreateSupervisorCommand request)
        {
            var username = $"{request.Email.Split('@', 2)[0]}";
            var user = await Mediator.Send(new GetUserQuery { Username = username });
            if (user == null)
            {
                request.UserId = await Mediator.Send(new CreateUserCommand { Role = (int)UserRole.Supervisor, Username = username, Password = "P@ssw0rd" });
            }
            else
            {
                request.UserId = user.Id;
            }

            return await Mediator.Send(request);
        }

        [RoleRequirementAttribute(UserRole.Supervisor)]
        [HttpPut("{id}/AddTopic")]
        public async Task<ActionResult<int>> AddTopic(int id, CreateTopicCommand request)
        {
            if (id != request.SupervisorId)
            {
                return BadRequest();
            }

            return await Mediator.Send(request);
        }

        [RoleRequirementAttribute(UserRole.Supervisor)]
        [HttpPost("{id}/UpdateTopic/{topicId}")]
        public async Task<ActionResult> UpdateTopic(int id, int topicId, UpdateTopicCommand request)
        {
            if (id != request.SupervisorId)
            {
                return BadRequest();
            }

            if (topicId != request.TopicId)
            {
                return BadRequest();
            }

            await Mediator.Send(request);

            return Ok();
        }

        [RoleRequirementAttribute(UserRole.Supervisor)]
        [HttpDelete("{id}/DeleteTopic/{topicId}")]
        public async Task<ActionResult> DeleteTopic(int id, int topicId, DeleteTopicCommand request)
        {
            if (id != request.SupervisorId)
            {
                return BadRequest();
            }

            if (topicId != request.TopicId)
            {
                return BadRequest();
            }

            await Mediator.Send(request);

            return Ok();
        }

        [RoleRequirementAttribute(UserRole.Supervisor)]
        [HttpPut("{id}/AddField")]
        public async Task<ActionResult<int>> AddField(int id, CreateFieldCommand request)
        {
            if (id != request.SupervisorId)
            {
                return BadRequest();
            }

            return await Mediator.Send(request);
        }

        [RoleRequirementAttribute(UserRole.Supervisor)]
        [HttpPost("{id}/UpdateField/{fieldId}")]
        public async Task<ActionResult> UpdateField(int id, int fieldId, UpdateFieldCommand request)
        {
            if (id != request.SupervisorId)
            {
                return BadRequest();
            }

            if (fieldId != request.FieldId)
            {
                return BadRequest();
            }

            await Mediator.Send(request);

            return Ok();
        }

        [RoleRequirementAttribute(UserRole.Supervisor)]
        [HttpDelete("{id}/DeleteField/{fieldId}")]
        public async Task<ActionResult> DeleteField(int id, int fieldId, DeleteFieldCommand request)
        {
            if (id != request.SupervisorId)
            {
                return BadRequest();
            }

            if (fieldId != request.FieldId)
            {
                return BadRequest();
            }

            await Mediator.Send(request);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<SupervisorsVm>> GetSupervisors(GetSupervisorsQuery request)
        {
            return await Mediator.Send(request);
        }

        [RoleRequirementAttribute(UserRole.Administrator)]
        [HttpPost("{id}/UpdateLimit")]
        public async Task<ActionResult> UpdateStudentLimit(int id, UpdateStudentLimitCommand request)
        {
            if(id != request.SupervisorId)
            {
                return BadRequest();
            }

            await Mediator.Send(request);

            return Ok();
        }
    }
}
