using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Common.Models
{
    public class ProfileData: IProfileData
    {
        public string GivenName { get; set; }
        public int ProfileId { get; set; }
        public User User { get; set; }
    }
}
