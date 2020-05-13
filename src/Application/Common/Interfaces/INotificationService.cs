using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace tti_graduation_work.Application.Common.Interfaces
{
    public interface INotificationService
    {
        void Notify(INotificationModel notification);
    }
}
