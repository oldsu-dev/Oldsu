using System.Threading.Tasks;
using Oldsu.Enums;
using Oldsu.Types;

namespace Oldsu.Services
{
    public interface IStatService
    {
        public Task<StatsWithRank?> GetStatsWithRankAsync(uint userId, Mode mode);
        public Task<StatsWithRank> AddStatsAsync(uint userid, Mode mode);
        public Task UpdateStatsAsync(Stats stats);
    }
}