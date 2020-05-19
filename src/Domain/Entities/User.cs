using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Enums;

namespace tti_graduation_work.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserStatus Status { get; set; }
        public Role Role { get; set; }
    }
}
