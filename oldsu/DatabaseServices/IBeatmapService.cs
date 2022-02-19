using System.Threading.Tasks;
using Oldsu.Types;

namespace Oldsu.DatabaseServices
{
    public interface IBeatmapService
    {
        public Task<Beatmap?> GetBeatmapAsync(string mapHash);
    }
}