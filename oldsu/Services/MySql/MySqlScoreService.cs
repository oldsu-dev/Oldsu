using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oldsu.Enums;
using Oldsu.Types;

namespace Oldsu.Services.MySql
{
    public class MySqlScoreService : DbContext, IScoreService
    {
        public IAsyncEnumerable<ScoreRow> GetScoresByMapHashAsync(string mapHash)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<ScoreRow> GetScoresByMapIdAsync(uint mapId)
        {
            throw new NotImplementedException();
        }

        public async Task AddScoreAsync(ScoreRow score)
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