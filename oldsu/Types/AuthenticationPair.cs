using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oldsu.Types
{
    public class AuthenticationPair
    {
        [Key]
        public uint UserID { get; set; }
        
        [ForeignKey(nameof(UserID))]
        public virtual UserInfo User { get; set; }
        
        public string Password { get; set; }
    }
}