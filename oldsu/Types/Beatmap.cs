using System.ComponentModel.DataAnnotations.Schema;

namespace Oldsu.Types
{
    public class Beatmap
    {
        public string BeatmapHash { get; set; }
        public int BeatmapID { get; set; }

        public int BeatmapsetID { get; set; }
        
        public virtual Beatmapset Beatmapset { get; set; }

        public float Rating { get; set; } 
    }
}