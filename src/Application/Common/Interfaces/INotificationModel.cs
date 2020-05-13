using System;
using System.Collections.Generic;
using System.Text;

namespace tti_graduation_work.Application.Common.Interfaces
{
    public interface INotificationModel
    {
        string Recepient { get; set; }
        string Content { get; set; }
        string Subject { get; set; }
    }
}
