using System.Collections.Generic;
using System.Threading.Tasks;
using Oldsu.Enums;
using Oldsu.Types;

namespace Oldsu.Services
{
    public interface IScoreService
    {
        public IAsyncEnumerable<ScoreRow> GetScoresByMapHashAsync(string mapHash);
        public IAsyncEnumerable<ScoreRow> GetScoresByMapIdAsync(uint mapId);
        public Task AddScoreAsync(ScoreRow score);
    }
}