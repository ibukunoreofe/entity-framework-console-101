using EFConsole101.Models;
using Microsoft.EntityFrameworkCore;

namespace DapperUsageConsole.Repositories
{
    public class UserRepository(ErDbContext context)
    {
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> AddAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user; // EF Core populates the ID after SaveChangesAsync is called.
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateEmailAsync(int id, string newEmail)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            user.Email = newEmail;
            await context.SaveChangesAsync();
            return true;
        }
    }
}
