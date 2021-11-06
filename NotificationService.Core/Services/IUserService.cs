using System.Threading.Tasks;
using NotificationService.Core.Entities;
using NotificationService.Core.Services.Inputs;

namespace NotificationService.Core.Services
{
    public interface IUserService
    {
        Task<User> CreateAsync(UserInput user);
        Task<bool> DeleteAsync(string userId);
    }
}
