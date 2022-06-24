using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oldsu.Types;

namespace Oldsu.Utils;

public interface IOnlineUserService
{
    public Task<List<OnlineUser?>> GetOnlineUsers();
}