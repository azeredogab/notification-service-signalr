using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NotificationService.Core.Entities;

namespace NotificationService.Core.Services
{
    public interface INotificationMessageService
    {
        Task<bool> Dispatch(Notification notification); 
    }
}
