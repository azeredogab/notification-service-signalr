using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationService.Core.Exceptions
{
    public class NotificationsLimitExhaustedException : Exception
    {
        public NotificationsLimitExhaustedException(string message): base(message)
        { }
    }
}
