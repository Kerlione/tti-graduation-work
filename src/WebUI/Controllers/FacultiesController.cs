using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tti_graduation_work.Application.Faculties.Queries;

namespace tti_graduation_work.WebUI.Controllers
{
    [Authorize]
    public class FacultiesController: ApiController
    {
        [HttpGet]
        public async Task<ActionResult<FacultiesVm>> Get()
        {
            return await Mediator.Send(new GetFacultiesQuery());
        }
    }
}
