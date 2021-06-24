using Microsoft.EntityFrameworkCore;
using Oldsu.Types;
using System;
using System.Threading.Tasks;

namespace Oldsu
{
    public class Database : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseMySql(
                Environment.GetEnvironmentVariable("OLDSU_DB_CONNECTION_STRING"),
                MySqlServerVersion.LatestSupportedServerVersion
            );

        public async Task<User> Authenticate(string username, string password)
        {
            return await this.Users.FromSqlRaw(
                "SELECT * FROM Users WHERE Username = {0} and Password = {1}",
                username, password
            ).FirstOrDefaultAsync();
        }

        public async Task Register(string username, string email, string password, string country) =>
            await this.Database.ExecuteSqlRawAsync(
               @"INSERT INTO `oldsu_test`.`Users`
               (`Username`, `Email`, `Password`, `Country`) 
               VALUES
               ({0}, {1}, {2}, {3});",

               username, email, password, country
            );

        public DbSet<User> Users { get; set; }
        public DbSet<Stats> Stats { get; set; }
    }
}
