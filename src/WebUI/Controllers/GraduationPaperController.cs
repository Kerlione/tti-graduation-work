using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
using tti_graduation_work.Application.Users.Queries.GetEntityId;
using tti_graduation_work.WebUI.Attributes;
using tti_graduation_work.WebUI.Enums;
using tti_graduation_work.WebUI.Filters;

namespace tti_graduation_work.WebUI.Controllers
{
    [Authorize]
    public class GraduationPaperController : ApiController
    {
        [RoleRequirementAttribute(UserRole.Supervisor)]
        [HttpPost]
        public async Task<ActionResult<GraduationPapersVm>> Get(GetGraduationPapersQuery request)
        {
            return await Mediator.Send(request);
        }

        //[RoleRequirementAttribute(UserRole.Student)]
        [HttpGet]
        public async Task<ActionResult<Application.GraduationPapers.Queries.GetPaper.GraduationPaperDto>> GetPaper()
        {
            var studentId = GetEntityId();
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
