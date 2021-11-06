using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationService.Core.Services.Inputs
{
    public class UserInput
    {
        public string Id { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
    }
}
