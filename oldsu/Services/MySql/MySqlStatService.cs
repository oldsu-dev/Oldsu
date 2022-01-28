using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oldsu.Enums;
using Oldsu.Types;

namespace Oldsu.Services.MySql
{
    public class MySqlStatService : DbContext, IStatService
    {
        private DbSet<StatsWithRank> StatsWithRank { get; set; }
        private DbSet<Stats> Stats { get; set; }

        public async Task<StatsWithRank> AddStatsAsync(uint userid, Mode mode)
        {
            await Database.ExecuteSqlRawAsync(@"INSERT INTO `Stats` (`userid`, `Mode`) VALUES ({0}, {1});", userid, mode);
            
            return (await GetStatsWithRankAsync(userid, mode))!;
        }

        public async Task<StatsWithRank?> GetStatsWithRankAsync(uint userId, Mode mode)
        {
            var stats = await StatsWithRank.Where(st => st.UserID == userId && st.Mode == mode).AsNoTracking().FirstOrDefaultAsync();

            Attach((Stats) stats);

            return stats;
        }

        // stats parameter not used since ef has tracking
        public async Task UpdateStatsAsync(Stats stats)
        {
            await SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(
                Environment.GetEnvironmentVariable("OLDSU_DB_CONNECTION_STRING")!,
                MySqlServerVersion.LatestSupportedServerVersion
            );
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatsWithRank>().ToTable("StatsWithRank");
        }
    }
}