using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Application.Common.Interfaces
{
    public interface IExternalUserModel
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Group { get; set; }
    }
}
