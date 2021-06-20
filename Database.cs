using Microsoft.EntityFrameworkCore;
using Oldsu.Types;
using System;

namespace Oldsu
{
    public class Database : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseMySql(
                Environment.GetEnvironmentVariable("OLDSU_DB_CONNECTION_STRING"),
                MySqlServerVersion.LatestSupportedServerVersion
            );

        public DbSet<User> Users { get; set; }
    }
}
