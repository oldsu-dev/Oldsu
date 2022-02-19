using System.Threading.Tasks;
using Oldsu.Types;

namespace Oldsu.DatabaseServices
{
    public interface IUserService
    {
        public Task<UserInfo?> AuthenticateAsync(string username, string passwordHash);
        public Task<UserInfo?> GetUserInfo(uint userId);
    }
}