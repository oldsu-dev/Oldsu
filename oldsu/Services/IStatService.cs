using System.Threading.Tasks;
using Oldsu.Types;

namespace Oldsu.Services
{
    public interface IStatService
    {
        public Task<StatsWithRank> GetStatsWithRankAsync();
    }
}