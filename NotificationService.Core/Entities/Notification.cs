using System;

namespace NotificationService.Core.Entities
{
    public class Notification
    {
        public string Id { get; set; }
        public string DestinationUserId { get; set; }
        public string SourceUserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string ImageUrl { get; set; }
        public bool Viewed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
