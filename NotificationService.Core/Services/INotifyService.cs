using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NotificationService.Core.Entities;
using NotificationService.Core.Services.DTO;

namespace NotificationService.Core.Services
{
    public interface INotifyService
    {
        Task<List<Notification>> NotifyAsync(NotificationInput notification);
        Task<List<Notification>> GetLastByUserAliasAsync(string userId, int limit);
        Task<int> GetCountUnviewedAsync(string userId);
    }
}
