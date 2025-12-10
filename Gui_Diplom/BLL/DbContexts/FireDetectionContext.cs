using BLL.DbModels;
using Microsoft.EntityFrameworkCore;

namespace BLL.DbContexts
{
    public class FireDetectionContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<VideoArchive> VideoArchives { get; set; }

        public DbSet<Detection> Detections { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer($"Server=DESKTOP-PVCQ3QK;Database=FireDetection;Trusted_Connection=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            builder.Entity<Session>()
                .HasIndex(u => u.AccessToken)
                .IsUnique();
        }
    }
}
