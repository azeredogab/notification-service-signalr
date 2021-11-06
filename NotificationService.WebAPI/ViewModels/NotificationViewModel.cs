using NotificationService.Core.Entities;

namespace NotificationService.WebAPI.ViewModels
{
    public class NotificationViewModel
    {
        public string id { get; set; }

        public static NotificationViewModel MapFromNotification(Notification notification)
        {
            return new NotificationViewModel()
            {
                id = notification.Id
            }; 
        }
    }
}
