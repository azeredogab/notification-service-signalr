using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Core.Exceptions;
using NotificationService.Core.Services;
using NotificationService.Core.Services.DTO;
using NotificationService.WebAPI.DTO;
using NotificationService.WebAPI.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace NotificationService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/notifications")]
    public class NotificationController : Controller
    {

        private readonly INotifyService _notificationService;

        public NotificationController(
            INotifyService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> Send([FromBody] NotificationInput notificationInput)
        {
            try
            {
                var notifications = await _notificationService.NotifyAsync(notificationInput);

                return Ok(new
                {
                    success = true,
                    created = notifications
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message); 
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetListByDestinationUser([FromQuery] NotificationFilter filter)
        {
            try
            {
                var limit = filter.limit > 0 ? filter.limit : 15; 
                var lastNotifications = await _notificationService.GetLastByUserAliasAsync(filter.destinationUserAlias, limit);
                var listViewModel = new List<NotificationViewModel>();

                foreach (var notification in lastNotifications)
                {
                    listViewModel.Add(NotificationViewModel.MapFromNotification(notification));
                }

                return Ok(new
                {
                    success = true,
                    data = listViewModel
                });
            }
            catch(UserNotFoundException e)
            {
                return NotFound(new
                {
                    success = false, 
                    error = e.Message
                }); 
            }
            catch(Exception e)
            {
                return BadRequest(new
                {
                    success = false,
                    error = e.Message
                });
            }
        }
    }
}
