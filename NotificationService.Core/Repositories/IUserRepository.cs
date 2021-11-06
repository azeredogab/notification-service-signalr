using System.Threading.Tasks;
using NotificationService.Core.Entities;

namespace NotificationService.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<bool> DeleteAsync(string userId);
        Task<User> UpdateAsync(User user);
        Task<User> GetByAliasAsync(string userId);
    }
}
