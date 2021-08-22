using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Oldsu.Types
{
    public class Rating
    {
        [Key]
        public uint UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual UserInfo User { get; set; }
        
        public int BeatmapsetID { get; set; }
        [ForeignKey("BeatmapsetID")]
        public virtual Beatmapset Beatmapset { get; set; }

        public float Rate { get; set; }
    }
}