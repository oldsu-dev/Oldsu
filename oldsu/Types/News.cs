using System;
using System.ComponentModel.DataAnnotations;

namespace Oldsu.Types
{
    public class News
    {
        [Key]
        public uint NewsID { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        public DateTime Date { get; set; }
    }
}