using System.Threading.Tasks;
using Oldsu.Enums;
using Oldsu.Types;

namespace Oldsu.Services
{
    public interface IScoreService
    {
        public Task<ScoreRow?> GetScoreAsync(uint userId, Mode mode);
        public Task AddScoreAsync(uint userid, Mode mode);
    }
}