using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Application.Common.Interfaces
{
    public interface IPasswordHasher
    {
        public string Hash(string password);
        public (bool Verified, bool NeedsUpgrade) Check(string hash, string password);
    }
}
