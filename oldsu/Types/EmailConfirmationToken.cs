using System.ComponentModel.DataAnnotations;

namespace Oldsu.Types
{
    public class EmailConfirmationToken
    {
        [Key]
        [StringLength(184)]
        public string Token { get; set; }
        
        [MaxLength(32)]
        public string PendingUsername { get; set; }
        
        [StringLength(60)]
        public string PendingPassword { get; set; }
        
        [MaxLength(byte.MaxValue + 1)]
        public string PendingEmail { get; set; }
        
        public byte Country { get; set; }
    }
}