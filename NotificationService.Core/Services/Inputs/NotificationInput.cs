using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationService.Core.Services.DTO
{
    public class NotificationInput
    {
        public string SourceUserAlias { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string ImageUrl { get; set; }
        public List<string> DestinationUsersAlias { get; set; }
    }
}
