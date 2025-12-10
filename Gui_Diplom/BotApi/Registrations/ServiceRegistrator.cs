using BLL.Services;

namespace BotApi.Registrations
{
    public class ServiceRegistrator
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<SessionService>();
            services.AddScoped<VideoArchiveService>();
            services.AddScoped<DetectionService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<StatisticsService>();
        }
    }
}
