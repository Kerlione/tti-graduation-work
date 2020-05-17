using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Common.Interfaces;

namespace tti_graduation_work.Infrastructure.Identity
{
    public class ExternalUserModel: IExternalUserModel
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Group { get; set; }
    }
}
