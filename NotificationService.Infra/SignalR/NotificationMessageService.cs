using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Core.Entities;
using NotificationService.Core.Services;

namespace NotificationService.Infra.SignalR
{
    public class NotificationMessageService : INotificationMessageService
    {
        private readonly IHubContext<NotificationHub> _signalR;

        public async Task<bool> Dispatch(Notification notification)
        {
            var userAlias = notification.DestinationUserId;
            var connection = ConnectionManager.GetConnectionByUserAlias(userAlias);

            if (connection == null)
            {
                return false;
            }

            await _signalR.Clients
                    .User(connection.ConnectionId)
                    .SendAsync(NotificationHub.Listener, notification);

            return true;
        }
    }
}
