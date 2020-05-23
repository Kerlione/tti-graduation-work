using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tti_graduation_work.Application.Users.Commands.LockUser;
using tti_graduation_work.Application.Users.Commands.UnlockUser;
using tti_graduation_work.Application.Users.Queries.GetUsers;
using tti_graduation_work.Application.Users.Queries.Sync;
using tti_graduation_work.WebUI.Attributes;
using tti_graduation_work.WebUI.Enums;

namespace tti_graduation_work.WebUI.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        [RoleRequirementAttribute(UserRole.Administrator)]
        [HttpPut("{id}/Lock")]
        public async Task<ActionResult> Lock(int id, LockUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [RoleRequirementAttribute(UserRole.Administrator)]
        [HttpPut("{id}/Unlock")]
        public async Task<ActionResult> Unlock(int id, UnlockUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [RoleRequirementAttribute(UserRole.Administrator)]
        [HttpGet("sync")]
        public async Task<ActionResult> Sync()
        {
            await Mediator.Send(new SyncQuery());
            return Ok();
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create()
        {
            return NoContent();
        }

        [RoleRequirementAttribute(UserRole.Administrator)]
        [HttpPost]
        public async Task<ActionResult<UsersVm>> GetUsers(GetUsersQuery request)
        {
            return await Mediator.Send(request);
        }
    }
}
