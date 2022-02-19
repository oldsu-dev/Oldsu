using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oldsu.Types;

namespace Oldsu.DatabaseServices.MySql
{
    public class MySqlBeatmapService : DbContext, IBeatmapService
    {
        private DbSet<Beatmap> Beatmaps { get; set; }

        public async Task<Beatmap?> GetBeatmapAsync(string mapHash)
        {
            return await Beatmaps
                .Where(b => b.BeatmapHash.Equals(mapHash))
                .Include(b => b.Beatmapset)
                .FirstOrDefaultAsync();
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