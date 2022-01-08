using System;
using Microsoft.EntityFrameworkCore;

namespace Oldsu.Services.MySql
{
    public class MySqlScoreService : DbContext, IUserService
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(
                Environment.GetEnvironmentVariable("OLDSU_DB_CONNECTION_STRING")!,
                MySqlServerVersion.LatestSupportedServerVersion
            );
        }
    }
}