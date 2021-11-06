using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Core.Entities;
using NotificationService.Core.Exceptions;
using NotificationService.Core.Repositories;
using NotificationService.Core.Services.DTO;

namespace NotificationService.Core.Services
{
    public class NotifyService : INotifyService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotificationMessageService _notificationMessageService; 

        public NotifyService(
            INotificationRepository notificationRepository,
            IUserRepository userRepository,
            INotificationMessageService notificationMessageService)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
            _notificationMessageService = notificationMessageService; 
        }

        public async Task<List<Notification>> NotifyAsync(NotificationInput notificationInput)
        {
            if (notificationInput.DestinationUsersAlias.Count == 0)
            {
                throw new ArgumentOutOfRangeException("É necessário ter pelo menos 1 destinatário para a notificação"); 
            }

            ValidateDestinations(notificationInput.DestinationUsersAlias);

            var notifications = new List<Notification>();

            foreach (var destination in notificationInput.DestinationUsersAlias)
            {
                var notification = new Notification()
                {
                    Id = Guid.NewGuid().ToString(),
                    DestinationUserId = destination,
                    SourceUserId = notificationInput.SourceUserAlias,
                    Title = notificationInput.Title,
                    Message = notificationInput.Message,
                    ImageUrl = notificationInput.ImageUrl,
                    Viewed = false,
                    CreatedAt = DateTime.Now
                };

                await _notificationRepository.CreateAsync(notification, "transaction");
                notifications.Add(notification); 
            }

            await _notificationRepository.Save();

            foreach (var notification in notifications)
            {
                await _notificationMessageService.Dispatch(notification);
            }

            return notifications; 
        }

        public async Task<List<Notification>> GetLastByUserAliasAsync(string userAlias, int limit = 15)
        {
            if (limit > 100)
            {
                throw new NotificationsLimitExhaustedException("Limite de notificações é 100.");
            }

            return await _notificationRepository.GetLastByUserAliasAsync(userAlias, limit); 
        }

        public async Task<int> GetCountUnviewedAsync(string userId)
        {
            return await _notificationRepository.GetCountUnviewedAsync(userId); 
        }

        private async void ValidateDestinations(List<string> destinations)
        {
            foreach (var desination in destinations)
            {
                var destinationExists = await _userRepository.GetByAliasAsync(desination);

                if (destinationExists == null)
                {
                    throw new UserNotFoundException($"Usuário {desination} não existe.");
                }
            }
        }
    }
}
