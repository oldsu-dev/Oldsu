using Microsoft.EntityFrameworkCore;
using Oldsu.Types;
using System;
using System.Linq;
using System.Threading.Tasks;
using Oldsu.Enums;

namespace Oldsu
{
    public class Database : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseMySql(
                    Environment.GetEnvironmentVariable("OLDSU_DB_CONNECTION_STRING")!,
                    MySqlServerVersion.LatestSupportedServerVersion
                );


        public DbSet<StatsWithRank> StatsWithRank { get; set; }

        public Task<StatsWithRank> GetStatsWithRankAsync(uint userId, uint mode) =>
            StatsWithRank.Where(st => st.UserID == userId && st.Mode == (Mode) mode).FirstOrDefaultAsync();
        
        public async Task<UserInfo?> AuthenticateAsync(string username, string password)
        {
            return await this.UserInfo.FromSqlRaw(
                "SELECT * FROM UserInfo WHERE Username = {0} and Password = {1}",
                username, password
            ).FirstOrDefaultAsync();
        }
        
        public async Task RegisterAsync(string username, string email, string password, string country) =>
            await this.Database.ExecuteSqlRawAsync(
               @"INSERT INTO `oldsu_test`.`UserInfo`
               (`Username`, `Email`, `Password`, `Country`) 
               VALUES
               ({0}, {1}, {2}, {3});",

               username, email, password, country
            );

        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Stats> Stats { get; set; }
    }
}
