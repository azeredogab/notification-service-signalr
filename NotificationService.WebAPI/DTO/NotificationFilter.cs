using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.WebAPI.DTO
{
    public class NotificationFilter
    {
        public string destinationUserAlias { get; set; }
        public int limit { get; set; }
    }
}
