using Microsoft.EntityFrameworkCore;

namespace Oldsu.Types
{
    [Keyless]
    public class BeatmapWithScoreCount
    {
        public string BeatmapHash { get; set; }
        public int BeatmapID { get; set; }

        public int BeatmapsetID { get; set; }
        
        public virtual Beatmapset Beatmapset { get; set; }

        public long ScoreCount { get; set; }

        public float Rating { get; set; } 
    }
}