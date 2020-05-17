using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using tti_graduation_work.WebUI.Enums;

namespace tti_graduation_work.WebUI.Filters
{
    public class RoleAttributeFilter : IAsyncAuthorizationFilter
    {
        readonly UserRole _role;

        public RoleAttributeFilter(UserRole role)
        {
            _role = role;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == _role.ToString());
            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
            return;
        }
    }
}
