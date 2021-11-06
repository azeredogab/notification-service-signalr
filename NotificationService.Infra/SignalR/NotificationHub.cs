using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Core.Repositories;
using NotificationService.Core.Services;

namespace NotificationService.Infra.SignalR
{
    public class NotificationHub : Hub
    {
        public static string Listener = "NotifiationEvent"; 
        private readonly INotifyService _notifyService;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotificationHub(
            INotifyService notifyService,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _notifyService = notifyService;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task OnConnectedAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var userAlias = httpContext.Request.Query["user"].ToString();
            var user = _userRepository.GetByAliasAsync(userAlias).GetAwaiter().GetResult(); 

            if (user == null)
            {
                Context.Abort();
            }

            var connection = new Connection()
            {
                ConnectionId = Context.ConnectionId,
                User = user
            };
            ConnectionManager.Connect(connection); 
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            ConnectionManager.Disconnect(connectionId); 
            return base.OnDisconnectedAsync(exception);
        }
    }
}
