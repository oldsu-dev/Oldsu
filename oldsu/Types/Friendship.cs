using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Oldsu.Types {
    [Keyless]
    public class Friendship {

        public uint UserID { get; set; }
        public uint FriendUserID { get; set; }
        [ForeignKey(nameof(UserID))]
        public UserInfo User { get; set; }
        [ForeignKey(nameof(FriendUserID))]
        public UserInfo FriendUser { get; set; }
    }
}
