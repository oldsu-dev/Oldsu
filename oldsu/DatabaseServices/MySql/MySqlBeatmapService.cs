﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oldsu.Types;
using Oldsu.Utils.Paginator;

namespace Oldsu.DatabaseServices.MySql
{
    public class MySqlBeatmapService : DbContext, IBeatmapService
    {
        private DbSet<Beatmap> Beatmaps { get; set; }
        private DbSet<Beatmapset> Beatmapsets { get; set; }

        public async Task<Beatmap?> GetBeatmapAsync(string mapHash)
        {
            return await Beatmaps
                .Where(b => b.BeatmapHash == mapHash)
                .Include(b => b.Beatmapset)
                .FirstOrDefaultAsync();
        }

        public async Task<Beatmapset?> GetBeatmapsetAsync(int beatmapId)
        {
            return await Beatmapsets
                .Where(b => b.BeatmapsetID == beatmapId)
                .FirstOrDefaultAsync();
        }

        public IPaginator<Beatmapset> GetBeatmapPaginator(int rowsPerPage) =>
            new MySqlEntityFrameworkPaginator<Beatmapset>(Beatmapsets, rowsPerPage);

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(
                Environment.GetEnvironmentVariable("OLDSU_DB_CONNECTION_STRING")!,
                MySqlServerVersion.LatestSupportedServerVersion
            );
        }
    }
}