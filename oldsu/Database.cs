using Microsoft.EntityFrameworkCore;
using Oldsu.Types;
using System;
using System.Linq;
using System.Threading.Tasks;
using Oldsu.Enums;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;

namespace Oldsu
{
    public class Database : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(
                Environment.GetEnvironmentVariable("OLDSU_DB_CONNECTION_STRING")!,
                MySqlServerVersion.LatestSupportedServerVersion
            );
        }

        public DbSet<StatsWithRank> StatsWithRank { get; set; }

        public Task<StatsWithRank> GetStatsWithRankAsync(uint userId, uint mode) =>
            StatsWithRank.Where(st => st.UserID == userId && st.Mode == (Mode) mode).FirstOrDefaultAsync();
        
        public async Task<UserInfo?> AuthenticateAsync(string username, string passwordHash)
        {
            var authenticationPair = await this.AuthenticationPairs
                .Where(auth => auth.User.Username == username).FirstOrDefaultAsync();

            if (authenticationPair == null)
                return null;
            
            if (BCrypt.Net.BCrypt.Verify(passwordHash, authenticationPair.Password))
                return authenticationPair.User;
            
            return null;
        }

        public async Task RegisterAsync(string username, string email, string passwordHash, byte country)
        {
            string bcrypt = BCrypt.Net.BCrypt.HashPassword(passwordHash);
            
            var transaction = await this.Database.BeginTransactionAsync();

            try
            {
                UserInfo userInfo = new UserInfo {Username = username, Email = email, Country = country};
                
                await this.UserInfo.AddAsync(userInfo);
                await this.SaveChangesAsync();

                await this.AuthenticationPairs.AddAsync(new AuthenticationPair
                    {UserID = userInfo.UserID, Password = bcrypt});
                
                await this.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await this.Database.RollbackTransactionAsync();
                throw;
            }
            
            
        }

        public async Task AddStatsAsync(uint userid, byte gamemode) =>
            await this.Database.ExecuteSqlRawAsync(
                @"INSERT INTO `Stats`
               (`userid`, `Mode`) 
               VALUES
               ({0}, {1});",

                userid, gamemode
            );

        public async Task TestAddMapAsync(string hash)
        {
            await this.Database.ExecuteSqlRawAsync(
                $@"INSERT INTO `BeatmapSets`
               (`Artist`, `Title`, `Source`, `Tags`, `RankingStatus`) 
               VALUES
               ('', '{hash}', '', '', 2);"
            );

            var beatmapset = await Beatmapsets
                .Where(b => b.Title == hash)
                .FirstAsync();

            await this.Database.ExecuteSqlRawAsync(
                @$"INSERT INTO `Beatmaps`
               (`BeatmapHash`, `BeatmapSetID`, `HP`, `CS`, `OD`, `SR`,`BPM`,`SliderMultiplier`,`Mode`,`Rating`) 
               VALUES
               ('{hash}', '{beatmapset.BeatmapsetID}', 0, 0, 0, 0, 0, 0, 0, 0);"
            );
        }

        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Stats> Stats { get; set; }
        
        public DbSet<Channel> AvailableChannels { get; set; }

        public DbSet<Beatmap> Beatmaps { get; set; }
        public DbSet<BeatmapWithScoreCount> BeatmapsWithScoreCount { get; set; }
        public DbSet<Beatmapset> Beatmapsets { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public DbSet<AuthenticationPair> AuthenticationPairs { get; set; }

        public DbSet<ScoreRow> Scores { get; set; }
        public DbSet<HighScoreWithRank> HighScoresWithRank { get; set; }

        public DbSet<Friendship> Friends { get; set; }
    }
}
