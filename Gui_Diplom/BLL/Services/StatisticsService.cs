using BLL.DbContexts;
using BLL.DbModels;
using BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class StatisticsService
    {
        private readonly FireDetectionContext context;

        public StatisticsService(FireDetectionContext context)
        {
            this.context = context;
        }

        public async Task<List<Statistics>> GetStatisticsByUserAsync(Guid sessionId, DateTime startDate, DateTime endDate)
        {
            var user = await context.Users.FirstAsync(u => u.Sessions.Select(s => s.AccessToken).Contains(sessionId));
            var userSessions = await context.Sessions.Where(s => s.UserId == user.Id).ToListAsync();

            var detections = await context.Detections
                .Where(d =>
                    userSessions.Select(s => s.SessionId).Contains(d.SessionId) &&
                    d.DetectionDate.Date >= startDate &&
                    d.DetectionDate.Date <= endDate
                )
                .GroupBy(d => d.DetectionDate.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Count = g.Count(),
                    Items = g.ToList()
                })
                .ToListAsync();

            var statistics = new List<Statistics>();

            foreach (var d in detections) 
            {
                statistics.Add(new Statistics
                {
                    DetectionDate = d.Date,
                    DetectionCount = d.Count
                });
            }

            return statistics;
        }

        public async Task<List<Statistics>> GetStatisticsForAdminAsync(DateTime startDate, DateTime endDate)
        {
            var detections = await context.Detections
                .Where(d =>
                    d.DetectionDate.Date >= startDate &&
                    d.DetectionDate.Date <= endDate
                )
                .GroupBy(d => d.DetectionDate.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Count = g.Count(),
                    Items = g.ToList()
                })
                .ToListAsync();

            var statistics = new List<Statistics>();

            foreach (var d in detections)
            {
                statistics.Add(new Statistics
                {
                    DetectionDate = d.Date,
                    DetectionCount = d.Count
                });
            }

            return statistics;
        }

        public async Task<List<Statistics>> GetStatisticsByUserId(int userId, DateTime startDate, DateTime endDate)
        {
            var userSessions = await context.Sessions.Where(s => s.UserId == userId).ToListAsync();

            var detections = await context.Detections
                .Where(d =>
                    userSessions.Select(s => s.SessionId).Contains(d.SessionId) &&
                    d.DetectionDate.Date >= startDate &&
                    d.DetectionDate.Date <= endDate
                )
                .GroupBy(d => d.DetectionDate.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Count = g.Count(),
                    Items = g.ToList()
                })
                .ToListAsync();

            var statistics = new List<Statistics>();

            foreach (var d in detections)
            {
                statistics.Add(new Statistics
                {
                    DetectionDate = d.Date,
                    DetectionCount = d.Count
                });
            }

            return statistics;
        }
    }
}
