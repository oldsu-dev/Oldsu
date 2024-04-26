using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Oldsu.Types
{
    public class Rating
    {
        public uint UserID { get; set; }
        [Key] public int BeatmapsetID { get; set; }
        
        [ForeignKey(nameof(UserID))] public virtual UserInfo User { get; set; }
        [ForeignKey(nameof(BeatmapsetID))] public virtual Beatmapset Beatmapset { get; set; }

        public float Rate { get; set; }
    }
}