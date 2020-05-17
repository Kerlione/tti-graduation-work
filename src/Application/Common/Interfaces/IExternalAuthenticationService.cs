using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Models;

namespace tti_graduation_work.Application.Common.Interfaces
{
    public interface IExternalAuthenticationService
    {
        Task<IExternalUserModel> AuthenticateAsync(UserModel user);
    }
}
