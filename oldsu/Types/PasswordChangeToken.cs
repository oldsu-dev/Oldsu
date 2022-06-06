using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oldsu.Types
{
    public class PasswordChangeToken
    {
        [Key]
        public string Token { get; set; }
        public uint UserID { get; set; }
        
        [ForeignKey(nameof(UserID))] public UserInfo User { get; set; }
    }
}