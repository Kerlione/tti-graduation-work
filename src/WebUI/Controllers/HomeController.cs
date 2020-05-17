using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tti_graduation_work.Application.News.Queries.GetNews;
using tti_graduation_work.Application.Users.Queries.GetProfile;

namespace tti_graduation_work.WebUI.Controllers
{
    [Authorize]
    public class HomeController: ApiController
    {
        [HttpPost]
        public async Task<ActionResult<ProfileVm>> GetProfile()
        {
            return await Mediator.Send(new GetProfileQuery() {Username = "St59206" });
        }

        [HttpGet]
        public async Task<ActionResult<NewsVm>> GetNews()
        {
            return NoContent();
        }
    }
}
