using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tti_graduation_work.Application.GraduationPapers.Queries.GetPapers;

namespace tti_graduation_work.WebUI.Controllers
{
    public class GraduationPaperController: ApiController
    {
        [HttpPost]
        public async Task<ActionResult<GraduationPapersVm>> Get(GetGraduationPapersQuery request)
        {
            return await Mediator.Send(request);
        }
    }
}
