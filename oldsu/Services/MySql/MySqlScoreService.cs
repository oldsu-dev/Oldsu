using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oldsu.Enums;
using Oldsu.Types;

namespace Oldsu.Services.MySql
{
    public class MySqlScoreService : DbContext, IScoreService
    {
        public async Task<ScoreRow?> GetScoreAsync(uint userId, Mode mode)
        {
            throw new NotImplementedException();
        }

        public async Task AddScoreAsync(uint userid, Mode mode)
        {
            throw new NotImplementedException();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(
                Environment.GetEnvironmentVariable("OLDSU_DB_CONNECTION_STRING")!,
                MySqlServerVersion.LatestSupportedServerVersion
            );
        }
    }
}