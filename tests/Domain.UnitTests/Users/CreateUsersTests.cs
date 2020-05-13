using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Entities;
using tti_graduation_work.Domain.Enums;

namespace tti_graduation_work.Domain.UnitTests.Users
{
    public class CreateUsersTests
    {
        [Test]
        public void ShouldCreateUser()
        {
            var user = new User
            {
                Role = Role.Administrator,
                Username = "Administrator"
            };

        }
    }
}
