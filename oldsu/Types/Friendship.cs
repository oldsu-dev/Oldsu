using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Oldsu.Types {
    public class Friendship {
        public uint UserID { get; set; }
        public uint FriendUserID { get; set; }

        public UserInfo User { get; set; }
        public UserInfo FriendUser { get; set; }
    }
}
