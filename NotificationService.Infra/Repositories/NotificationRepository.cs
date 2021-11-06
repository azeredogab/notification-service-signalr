using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificationService.Core.Entities;
using NotificationService.Core.Exceptions;
using NotificationService.Core.Repositories;
using NotificationService.Infra.DataContexts;

namespace NotificationService.Infra.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        
        public NotificationRepository(
            DataContext context,
            IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository; 
        }

        public async Task<Notification> CreateAsync(Notification notification, string mode = "auto")
        {
            await _context.notifications.AddAsync(notification);

            if (mode == "auto")
            {
                await Save();
            }

            return notification;
        }

        public async Task<int> GetCountUnviewedAsync(string userAlias)
        {
            return await _context.notifications.CountAsync(x => x.DestinationUserId == userAlias);
        }

        public async Task<List<Notification>> GetLastByUserAliasAsync(string userAlias, int limit = 15)
        {
            if (await _userRepository.GetByAliasAsync(userAlias) == null)
            {
                throw new UserNotFoundException("Usuário não existe."); 
            }

            return await _context.notifications
                .Join(
                    _context.users, 
                    notifications => notifications.DestinationUserId, 
                    user => user.Alias,
                    (notifications, user) => new { notifications, user })
                .Where(x => 
                    x.notifications.DestinationUserId == userAlias
                    && x.user.Active == true)
                .OrderByDescending(x => 
                    x.notifications.CreatedAt)
                .Select(x => 
                    x.notifications)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
