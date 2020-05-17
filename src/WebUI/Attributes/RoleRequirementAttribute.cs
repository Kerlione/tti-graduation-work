using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tti_graduation_work.WebUI.Enums;
using tti_graduation_work.WebUI.Filters;

namespace tti_graduation_work.WebUI.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class RoleRequirementAttribute: TypeFilterAttribute
    {
        public RoleRequirementAttribute(UserRole role) : base(typeof(RoleAttributeFilter))
        {
            Arguments = new object[] { role };
        }
    }
}
