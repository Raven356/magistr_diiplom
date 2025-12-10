using BLL.DbContexts;
using BLL.DbModels;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class SessionService
    {
        private FireDetectionContext context;

        public SessionService(FireDetectionContext context)
        {
            this.context = context;
        }

        public async Task<Session> CreateSessionAsync(Session session)
        {
            var result = await context.Sessions.AddAsync(session);
            await context.SaveChangesAsync();

            return result.Entity;
        } 

        public async Task ExpireOldSessions(int userId)
        {
            var oldSessions = context.Sessions.Where(s => s.UserId == userId);

            foreach (var session in oldSessions) 
            { 
                session.IsExpired = true;
            }

            await context.SaveChangesAsync();
        }

        public async Task<Session> GetSessionByGuidAsync(Guid sessionGuid)
        {
            return await context.Sessions.FirstOrDefaultAsync(x => x.AccessToken.Equals(sessionGuid));
        }
    }
}
