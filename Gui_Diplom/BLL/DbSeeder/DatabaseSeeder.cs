using BLL.DbContexts;
using BLL.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BLL.DbSeeder
{
    public class DatabaseSeeder
    {
        private readonly FireDetectionContext _db;

        public DatabaseSeeder(FireDetectionContext context)
        {
            _db = context;
        }

        public async Task SeedAsync()
        {
            await _db.Database.MigrateAsync();

            if (!await _db.Users.AnyAsync(u => u.IsAdmin == true))
            {
                _db.Users.Add(new User
                {
                    UserName = "admin",
                    Password = Hash("admin"),
                    AuthType = 0,
                    IsAdmin = true
                });

                await _db.SaveChangesAsync();
            }
        }

        private static string Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                var sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
    }
}
