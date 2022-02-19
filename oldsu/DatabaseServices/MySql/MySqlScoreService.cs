using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oldsu.Types;

namespace Oldsu.DatabaseServices.MySql
{
    public class MySqlScoreService : DbContext, IScoreService
    {
        private DbSet<ScoreRow> Scores { get; set; }

        public IAsyncEnumerable<ScoreRow> GetScoresByMapHashAsync(string mapHash)
        {
            return Scores
                .Include(s => s.Beatmap)
                .Where(s => s.BeatmapHash.Equals(mapHash))
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<ScoreRow> GetScoresByMapIdAsync(uint mapId)
        {
            return Scores
                .Include(s => s.Beatmap)
                .Where(s => s.Beatmap.BeatmapID.Equals(mapId))
                .AsAsyncEnumerable();
        }

        public async Task AddScoreAsync(ScoreRow score)
        {
            await Scores.AddAsync(score);

            await SaveChangesAsync();
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