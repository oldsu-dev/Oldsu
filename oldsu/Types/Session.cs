﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oldsu.Types
{
    public class Session
    {
        [Key]
        public string Token { get; set; }
        
        public uint UserID { get; set; }
        
        [ForeignKey(nameof(UserID))]
        public UserInfo UserInfo { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}