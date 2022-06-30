using System.Threading.Tasks;
using Oldsu.Types;

namespace Oldsu.DatabaseServices
{
    public interface IUserService
    {
        public Task<UserInfo?> AuthenticateAsync(string username, string passwordHash);
        public Task<UserInfo?> GetUserInfoAsync(uint userId);
        public Task SetUserBanByName(string userName, bool isBanned);
    }
}