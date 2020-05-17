using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Common.Interfaces
{
    public interface IAuthRepository
    {
        public string CreateToken(User user);
    }
}
