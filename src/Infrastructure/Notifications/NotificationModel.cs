using System;
using System.Collections.Generic;
using System.Text;
using tti_graduation_work.Application.Common.Interfaces;

namespace tti_graduation_work.Infrastructure.Notifications
{
    public class NotificationModel: INotificationModel
    {
        public string Recepient { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
    }
}
