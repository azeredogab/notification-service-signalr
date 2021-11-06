using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificationService.Core.Entities;
using NotificationService.Core.Repositories;
using NotificationService.Infra.DataContexts;

namespace NotificationService.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context; 
        }

        public async Task<User> CreateAsync(User user)
        {
            var userExists = await GetByAliasAsync(user.Alias); 

            if (userExists != null)
            {
                throw new Exception("Usuário já existe. Não é possível cadastra-lo novamente."); 
            }

            await _context.users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user; 
        }

        public async Task<bool> DeleteAsync(string userId)
        {
            var user = await _context.users.FirstOrDefaultAsync(x => x.Id == userId); 

            if (user == null)
            {
                throw new Exception("Usuário não existe.");
            }

            _context.users.Update(user.Disable());
            var isUpdated = await _context.SaveChangesAsync();

            return isUpdated > 0 ? true : false;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    
        public async Task<User> GetByAliasAsync(string userAlias)
        {
            return await _context.users.FirstOrDefaultAsync(x => x.Alias == userAlias && x.Active == true);
        }
    }
}
