using System.Collections.Generic;
using System.Threading.Tasks;
using Oldsu.Enums;
using Oldsu.Types;

namespace Oldsu.DatabaseServices
{
    public interface IScoreService
    {
        public Task<List<HighScoreWithRank>?> GetHighScoresOnMap(string mapHash, Mode gamemode, int limit);
        public Task<HighScoreWithRank?> GetHighScoreOnMap(string mapHash, Mode gamemode, uint userId);
        public Task AddScoreAsync(ScoreRow score);
    }
}