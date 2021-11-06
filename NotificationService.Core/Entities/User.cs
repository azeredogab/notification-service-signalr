using System;

namespace NotificationService.Core.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public User Disable()
        {
            Active = false;
            return this; 
        }
    }
}
