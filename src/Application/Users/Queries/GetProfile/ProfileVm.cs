using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Application.Users.Queries.GetProfile
{
    public class ProfileVm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<string> Emails { get; set; }
        public IList<string> PhoneNumbers { get; set; }
        public string Faculty { get; set; }
        public string Programe { get; set; }
    }
}
