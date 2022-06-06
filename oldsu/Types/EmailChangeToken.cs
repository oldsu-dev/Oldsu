using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oldsu.Types
{
    public class EmailChangeToken
    {
        [Key]
        public string Token { get; set; }
        public uint UserID { get; set; }
        
        public string Email { get; set; }
        
        [ForeignKey(nameof(UserID))] public UserInfo User { get; set; }
    }
}