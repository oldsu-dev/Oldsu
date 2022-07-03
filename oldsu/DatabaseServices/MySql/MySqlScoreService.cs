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
        private DbSet<HighScoreWithRank?> HighScoresWithRank { get; set; }

        public async Task<List<HighScoreWithRank?>> GetHighScoresOnMapAsync(string mapHash, Mode gamemode, int limit)
        {
            return await HighScoresWithRank
                .Where(s => s.BeatmapHash == mapHash &&
                            s.Gamemode == (byte)gamemode &&
                            s.Passed)
                .Include(s => s.User)
                .OrderByDescending(s => s.Score)
                .Take(limit)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<HighScoreWithRank?> GetHighScoreOnMapAsync(string mapHash, Mode gamemode, uint userId)
        {
            return await HighScoresWithRank
                .Where(s => s.BeatmapHash == mapHash &&
                            s.Gamemode == (byte)gamemode &&
                            s.UserId == userId &&
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