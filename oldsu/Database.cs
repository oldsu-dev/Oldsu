using Microsoft.EntityFrameworkCore;
using Oldsu.Types;
using System;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using Oldsu.Enums;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable 8618

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

        public async Task UpdateInformation(uint userId, string? occupation, string? interests, DateTime? birthday,
            string? discord, string? twitter, string? website)
        {
            var transaction = await Database.BeginTransactionAsync();

            try
            {
                var info = await UserPages
                    .Where(up => up.UserID == userId)
                    .FirstOrDefaultAsync();

                info.Occupation = occupation;
                info.Interests = interests;
                info.Birthday = birthday;
                info.Discord = discord;
                info.Twitter = twitter;
                info.Website = website;
                
                await SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        
        public async Task UpdateBBCode(uint userId, string? BBCode)
        {
            var transaction = await Database.BeginTransactionAsync();

            try
            {
                var info = await UserPages
                    .Where(up => up.UserID == userId)
                    .FirstOrDefaultAsync();

                info.BBText = BBCode;
                
                await SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Session?> GetWebSession(string sessionId) => 
            await SessionTokens.Where(s => s.SessionId == sessionId)
                .Include(s => s.UserInfo)
                .FirstOrDefaultAsync();

        public async Task AddWebSession(string sessionId, uint userId, DateTime expiration)
        {
            // remove old sessions
            var oldSessions = await SessionTokens
                .Where(s => s.UserID == userId)
                .AsNoTracking()
                .ToListAsync();

            foreach (var oldSession in oldSessions)
                await RemoveWebSession(oldSession.SessionId);
            
            var transaction = await Database.BeginTransactionAsync();

            try
            {
                await SessionTokens.AddAsync(new Session
                {
                    SessionId = sessionId,
                    UserID = userId,
                    ExpiresAt = expiration
                });
                
                await SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task RemoveWebSession(string sessionId)
        {
            var transaction = await Database.BeginTransactionAsync();

            try
            {
                SessionTokens.Remove(new Session
                {
                    SessionId = sessionId,
                });
                
                await SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<UserInfo?> AuthenticateAsync(string username, string passwordHash)
        {
            var authenticationPair = await this.AuthenticationPairs
                .Where(auth => auth.User.Username == username).Include(auth => auth.User).FirstOrDefaultAsync();

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
                await transaction.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        ///     Checks if user is valid for registration.
        /// </summary>
        public async Task<RegisterAttemptResult> ValidateRegistrationAttempt(string username, string ip)
        {
            // todo check hwid
            var user = await this.UserInfo
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();

            if (user != null)
                return RegisterAttemptResult.UsernameAlreadyExists;
            
            /*
             var security = await this.UserSecurityInfo.Where(s=>s.Ip == ip || s.Hwid == hwid)
             
            check...
            */
            return RegisterAttemptResult.RegisterSuccessful;
        }

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

        public DbSet<AuthenticationPair> AuthenticationPairs { get; set; }

        public DbSet<ScoreRow> Scores { get; set; }
        public DbSet<HighScoreWithRank> HighscoresWithRank { get; set; }
        
        public DbSet<News> News { get; set; }

        public DbSet<Friendship> Friends { get; set; }
        
        public DbSet<UserPage> UserPages { get; set; }
        public DbSet<RankHistory> RankHistory { get; set; }
        
        public DbSet<Badge> Badges { get; set; }

        public DbSet<Session> SessionTokens { get; set; }
    }
}
