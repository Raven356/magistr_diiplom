using BLL.DbContexts;
using BLL.DbModels;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class UserService
    {
        private FireDetectionContext context;

        public UserService(FireDetectionContext context) 
        {
            this.context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var result = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<User> GetUserByTelegramUserIdAsync(string telegramId)
        {
            return await context.Users.FirstOrDefaultAsync(x => !string.IsNullOrEmpty(x.TelegramId) && x.TelegramId.Equals(telegramId));
        }

        public async Task<User> GetUserByIdAync(int id)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByNameAsync(string userName)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.UserName.Equals(userName));
        }

        public async Task<User> GetUserByGoogleUserIdAsync(string googleUserId)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.GoogleUserId.Equals(googleUserId));
        }

        public async Task<User> GetUserBySessionId(Guid sessionId)
        {
            var session = await context.Sessions.FirstOrDefaultAsync(x => x.AccessToken.Equals(sessionId));

            return await context.Users.FirstOrDefaultAsync(x => x.Id == session.UserId);
        }

        public async Task UpdateUserAsync(User user)
        {
            var existing = await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (!string.IsNullOrEmpty(user.Password))
            {
                existing.Password = user.Password;
            }

            existing.UserName = user.UserName;

            existing.EmailAddress = user.EmailAddress;

            existing.NotificatonType = user.NotificatonType;

            await context.SaveChangesAsync();
        }

        public async Task UpdateUserTelegramId(int userId, string telegramId)
        {
            var existing = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            existing.TelegramId = telegramId;

            await context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = context.Users.Where(u => !u.IsAdmin);

            return await users.ToListAsync();
        }

        public async Task DeleteById(int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(u =>u.Id == id);
            context.Users.Remove(user);

            await context.SaveChangesAsync();
        }
    }
}
