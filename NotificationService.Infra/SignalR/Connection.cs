using NotificationService.Core.Entities;

namespace NotificationService.Infra.SignalR
{
    public class Connection
    {
        public string ConnectionId { get; set; }
        public User User { get; set; }
    }
}
