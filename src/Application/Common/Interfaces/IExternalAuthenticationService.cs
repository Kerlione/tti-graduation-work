using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Common.Models;

namespace tti_graduation_work.Application.Common.Interfaces
{
    public interface IExternalAuthenticationService
    {
        void Authenticate(UserModel user);
    }
}
