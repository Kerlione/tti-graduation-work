using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Application.Common.Exceptions
{
    public class NotAccessibleEntityException: Exception
    {
        public NotAccessibleEntityException()
            : base()
        {
        }

        public NotAccessibleEntityException(string message)
            : base(message)
        {
        }

        public NotAccessibleEntityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
