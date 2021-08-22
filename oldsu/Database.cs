using Microsoft.EntityFrameworkCore;
using Oldsu.Types;
using System;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
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
        
        public async Task RegisterAsync(string username, string email, string password, byte country) =>
            await this.Database.ExecuteSqlRawAsync(
               @"INSERT INTO `UserInfo`
               (`Username`, `Email`, `Password`, `Country`) 
               VALUES
               ({0}, {1}, {2}, {3});",

               username, email, password, country
            );

        public async Task AddStatsAsync(uint userid, byte gamemode) =>
            await this.Database.ExecuteSqlRawAsync(
                @"INSERT INTO `Stats`
               (`userid`, `Mode`) 
               VALUES
               ({0}, {1});",

                userid, gamemode
            );

        [Obsolete("dont use, vulnerable to sql injections.")]
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

        public async Task ExecuteStatUpdate(Stats stats)
        {
            await this.Database.ExecuteSqlRawAsync(
                @"UPDATE `Stats` SET 
                  RankedScore = {0}, TotalScore = {1}, Accuracy = {2}, Playcount = {3}, CountSSH = {4}, CountSS = {5}, CountSH = {6}, CountS = {7}, CountA = {8}, CountB = {9}, CountC = {10}, CountD = {11}, Hit300 = {12}, Hit100 = {13}, Hit50 = {14}, HitMiss = {15}
                  WHERE UserID = {16} AND Mode = {17};",
                stats.RankedScore, stats.TotalScore, stats.Accuracy, stats.Playcount, stats.CountSSH, stats.CountSS,
                stats.CountSH, stats.CountS, stats.CountA, stats.CountB, stats.CountC, stats.CountD, stats.Hit300, stats.Hit100,
                stats.Hit50, stats.HitMiss, stats.UserID, stats.Mode);
        }
        
        public async Task ExecuteStatUpdate(StatsWithRank stats)
        {
            await this.Database.ExecuteSqlRawAsync(
                @"UPDATE `Stats` SET 
                  RankedScore = {0}, TotalScore = {1}, Accuracy = {2}, Playcount = {3}, CountSSH = {4}, CountSS = {5}, CountSH = {6}, CountS = {7}, CountA = {8}, CountB = {9}, CountC = {10}, CountD = {11}, Hit300 = {12}, Hit100 = {13}, Hit50 = {14}, HitMiss = {15}
                  WHERE UserID = {16} AND Mode = {17};",
                stats.RankedScore, stats.TotalScore, stats.Accuracy, stats.Playcount, stats.CountSSH, stats.CountSS,
                stats.CountSH, stats.CountS, stats.CountA, stats.CountB, stats.CountC, stats.CountD, stats.Hit300, stats.Hit100,
                stats.Hit50, stats.HitMiss, stats.UserID, stats.Mode);
        }

        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Stats> Stats { get; set; }
        
        public DbSet<Channel> AvailableChannels { get; set; }

        public DbSet<Beatmap> Beatmaps { get; set; }
        public DbSet<BeatmapWithScoreCount> BeatmapsWithScoreCount { get; set; }
        public DbSet<Beatmapset> Beatmapsets { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        
        public DbSet<ScoreRow> Scores { get; set; }
        public DbSet<HighScoreWithRank> HighScoresWithRank { get; set; }

        public DbSet<Friendship> Friends { get; set; }
    }
}
