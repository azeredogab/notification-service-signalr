using System;

namespace NotificationService.Core.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message)
        { }
    }
}
