using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using tti_graduation_work.WebUI.Enums;

namespace tti_graduation_work.WebUI.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected bool ValidateRole(UserRole role)
        {
            return User.IsInRole(role.ToString());
        }
        /// <summary>
        /// NameIdentifier contains the ID of Student or Supervisor entities (for Admin, the UserID will be returned)
        /// </summary>
        /// <returns>Entity ID</returns>
        protected int GetEntityId()
        {
            return Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }

        /// <summary>
        /// Get Username from Identity token
        /// </summary>
        /// <returns>Authenticated user's Name claim value</returns>
        protected string GetUsername()
        {
            return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
        }
    }
}
