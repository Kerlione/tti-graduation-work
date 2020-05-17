using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tti_graduation_work.Application.Users.Commands.AuthenticateUser;

namespace tti_graduation_work.WebUI.Controllers
{
    public class AuthenticationController : ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserIdentity>> Login(AuthenticateUserCommand request)
        {
            var userIdentity = await Mediator.Send(request);
            if(userIdentity == null)
            {
                return Unauthorized();
            }
            return userIdentity;
        }

    }
}
