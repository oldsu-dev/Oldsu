using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oldsu.Enums;
using Oldsu.Types;

namespace Oldsu.DatabaseServices.MySql
{
    public class MySqlScoreService : DbContext, IScoreService
    {
        private DbSet<ScoreRow> Scores { get; set; }
        private DbSet<HighScoreWithRank?> HighScoreWithRanks { get; set; }

        public async Task<List<HighScoreWithRank>?> GetHighScoresOnMap(string mapHash, Mode gamemode)
        {
            throw new NotImplementedException();
        }

        public async Task<HighScoreWithRank?> GetHighScoreOnMap(string mapHash, Mode gamemode, uint userId)
        {
            return await HighScoreWithRanks
                .Where(s => s.BeatmapHash.Equals(mapHash) &&
                            s.Gamemode.Equals(gamemode) &&
                            s.UserId.Equals(userId) &&
                            s.Passed)
                .Include(s => s.User)
                .FirstOrDefaultAsync();
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