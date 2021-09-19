using System;
using System.ComponentModel.DataAnnotations;

namespace Oldsu.Types {
    public class UserPage {
        [Key] 
        public int UserID { get; set; }
        
        public DateTime? Birthday { get; set; }

        [StringLength(32)]
        public string? Occupation { get; set; }
        
        [StringLength(32)]
        public string? Interests { get; set; }
        
        [StringLength(32)]
        public string? Website { get; set; }
        
        [StringLength(32)]
        public string? Twitter { get; set; }
        
        [StringLength(32)]
        public string? Discord { get; set; }
        
        [StringLength(32)]
        public string? Title { get; set; }
        
        public string? BBText { get; set; }
    }
}