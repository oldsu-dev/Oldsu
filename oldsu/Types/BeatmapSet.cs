using System;

namespace Oldsu.Types
{
    public class Beatmapset
    {
        public int BeatmapsetID { get; set; }
        public uint? OriginalBeatmapsetID { get; set; }

        public uint? CreatorID { get; set; }
        public string CreatorName { get; set; }

        public string Title { get; set; }
        public string Artist { get; set; }
        
        public string Source { get; set; }
        public string Tags { get; set; }

        public string? DisplayedTitle { get; set; }

        public byte RankingStatus { get; set; }

        public string? RankedBy { get; set; }

        public DateTime SubmittedAt { get; set; }
        public DateTime? RankedAt { get; set; }

        public float Rating { get; set; }
        public uint RatingCount { get; set; }
        
        public uint LanguageId { get; set; }
        public bool IsHidden { get; set; }
    }
}