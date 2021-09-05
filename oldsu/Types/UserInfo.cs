using Oldsu.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Oldsu.Types
{
    public class UserInfo
    {
        [Key]
        public uint UserID { get; set; }

        [Required]
        [StringLength(32)]
        public string Username { get; set; }

        [Required]
        public byte Country { get; set; }

        [JsonIgnore]
        public bool Banned { get; set; }
        [JsonIgnore]
        public string? BannedReason { get; set; }

        [StringLength(256)]
        [JsonIgnore]
        public string Email { get; set; }

        public Privileges Privileges { get; set; }
    }
}
