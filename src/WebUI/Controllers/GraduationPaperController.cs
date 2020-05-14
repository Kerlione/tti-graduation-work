using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tti_graduation_work.Application.GraduationPapers.Commands.CreatePaper;
using tti_graduation_work.Application.GraduationPapers.Queries.GetPaper;
using tti_graduation_work.Application.GraduationPapers.Queries.GetPapers;
using tti_graduation_work.Application.GraduationPapers.Queries.PaperExists;
using tti_graduation_work.Application.Steps.Commands.CreateSteps;
using tti_graduation_work.WebUI.Enums;

namespace tti_graduation_work.WebUI.Controllers
{
    public class GraduationPaperController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<GraduationPapersVm>> Get(GetGraduationPapersQuery request)
        {
            return await Mediator.Send(request);
        }

        [HttpGet]
        public async Task<ActionResult<Application.GraduationPapers.Queries.GetPaper.GraduationPaperDto>> GetPaper()
        {
            var studentId = 5; // TODO move to get from DB using token value
            var paperExists = await Mediator.Send(new PaperExistsQuery { StudentId = studentId });
            if (!paperExists)
            {
                var graduationPaperId = await Mediator.Send(new CreatePaperCommand { StudentId = studentId, PaperType = (int)PaperType.Bachelor });
                if (graduationPaperId != 0)
                {
                    await Mediator.Send(new CreateStepsCommand { GraduationPaperId = graduationPaperId, PaperType = (int)PaperType.Bachelor });
                }
            }

            return await Mediator.Send(new GetPaperQuery { StudentId = studentId });
        }
    }
}
