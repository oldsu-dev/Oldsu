using System.Threading.Tasks;
using Oldsu.Types;

namespace Oldsu.Storage
{
    public interface IOnlineUserRepository
    {
        public Task<UserInfo?> GetUser(uint id);
        public Task AddUser(UserInfo userInfo);
        public Task RemoveUser(uint id);
    }
}