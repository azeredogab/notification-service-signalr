using System;
using System.Threading.Tasks;
using NotificationService.Core.Entities;
using NotificationService.Core.Repositories;
using NotificationService.Core.Services.Inputs;

namespace NotificationService.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository; 
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository; 
        }

        public async Task<User> CreateAsync(UserInput userInput)
        {
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Alias = userInput.Alias,
                Name = userInput.Name,
                Email = userInput.Email,
                PhotoUrl = userInput.PhotoUrl,
                Active = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            return await _userRepository.CreateAsync(user);
        }

        public async Task<bool> DeleteAsync(string userId)
        {
            return await _userRepository.DeleteAsync(userId);
        }
    }
}
