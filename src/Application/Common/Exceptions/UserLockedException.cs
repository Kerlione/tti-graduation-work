using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Application.Common.Exceptions
{
    public class UserLockedException: Exception
    {
        public UserLockedException()
            : base()
        {
        }

        public UserLockedException(string message)
            : base(message)
        {
        }

        public UserLockedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public UserLockedException(User user): base($"{user.Username} is locked")
        {

        }
    }
}
