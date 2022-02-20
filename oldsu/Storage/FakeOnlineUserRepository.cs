using System.Threading.Tasks;
using Oldsu.Types;

namespace Oldsu.Storage
{
    public class FakeOnlineUserRepository : IOnlineUserRepository
    {
        public async Task<UserInfo?> GetUser(uint id)
        {
            return null;
        }

        public async Task AddUser(UserInfo userInfo) { }

        public async Task RemoveUser(uint id) { }
    }
}