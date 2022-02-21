using System.Threading.Tasks;
using Oldsu.Types;
using Oldsu.Utils.Paginator;

namespace Oldsu.DatabaseServices
{
    public interface IBeatmapService
    {
        public Task<Beatmap?> GetBeatmapAsync(string mapHash);
        public Task<Beatmapset?> GetBeatmapsetAsync(int beatmapId);
        public IPaginator<Beatmapset> GetBeatmapPaginator(int rowsPerPage);
    }
}