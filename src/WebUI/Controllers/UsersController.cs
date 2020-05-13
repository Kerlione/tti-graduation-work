using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tti_graduation_work.Application.Users.Commands.LockUser;
using tti_graduation_work.Application.Users.Commands.UnlockUser;
using tti_graduation_work.Application.Users.Queries.GetUsers;

namespace tti_graduation_work.WebUI.Controllers
{
    public class UsersController : ApiController
    {
        [HttpPut("lock/{id}")]
        public async Task<ActionResult> Lock(int id, LockUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("unlock/{id}")]
        public async Task<ActionResult> Unlock(int id, UnlockUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpGet("sync")]
        public async Task<ActionResult<int>> Sync()
        {
            // TODO: implement sync logic

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Create()
        {
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<UsersVm>> Get()
        {
            return await Mediator.Send(new GetUsersQuery());
        }
    }
}
