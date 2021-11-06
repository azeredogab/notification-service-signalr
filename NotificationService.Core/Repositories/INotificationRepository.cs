using System.Collections.Generic;
using System.Threading.Tasks;
using NotificationService.Core.Entities;

namespace NotificationService.Core.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> CreateAsync(Notification notification, string mode);
        Task<List<Notification>> GetLastByUserAliasAsync(string userId, int limit);
        Task<int> GetCountUnviewedAsync(string userId);
        Task<int> Save();
    }
}
