using Oldsu.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

        public bool Banned { get; set; }
        public string? BannedReason { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public Privileges Privileges { get; set; }
    }
}
