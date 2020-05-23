using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tti_graduation_work.Application.Students.Commands;
using tti_graduation_work.Application.Students.Queries.GetStudents;
using tti_graduation_work.Application.Students.Queries.StudentExists;
using tti_graduation_work.Application.Users.Commands.CreateUser;
using tti_graduation_work.Application.Users.Queries.GetUser;
using tti_graduation_work.WebUI.Enums;

namespace tti_graduation_work.WebUI.Controllers
{
    [Authorize]
    public class StudentsController: ApiController
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateStudentCommand command)
        {
            var username = $"St{command.ExternalId}";
            var user = await Mediator.Send(new GetUserQuery { Username = username });
            if(user == null)
            {
                command.UserId = await Mediator.Send(new CreateUserCommand { Role = (int)UserRole.Student, Username = username, Password = "P@ssw0rd" });
            }
            else
            {
                command.UserId = user.Id;
            }

            if(await Mediator.Send(new StudentExistsCommand { ExternalId = command.ExternalId }))
            {
                return NoContent();
            }

            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<ActionResult<StudentsVm>> Get()
        {
            return await Mediator.Send(new GetStudentsQuery());
        }
    }
}
