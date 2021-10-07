using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oldsu.Types
{
    public class Session
    {
        [Key]
        public string SessionId { get; set; }
        
        public uint UserID { get; set; }
        
        [ForeignKey(nameof(UserID))]
        public UserInfo UserInfo { get; set; }
    }
}