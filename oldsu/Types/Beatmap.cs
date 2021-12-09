using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oldsu.Enums;

namespace Oldsu.Types
{
    public class Beatmap
    {
        [Key]
        public string BeatmapHash { get; set; }
        public int BeatmapID { get; set; }

        public uint? OriginalBeatmapID { get; set; }

        public virtual Beatmapset Beatmapset { get; set; }

        public int BeatmapsetID { get; set; }

        public string DifficultyName { get; set; }

        public DateTime LastUpdateAt { get; set; }

        public float Hp { get; set; }
        public float Cs { get; set; }
        public float Od { get; set; }
        public float Ar { get; set; }
        public float Sr { get; set; }

        public float Bpm { get; set; }

        public double SliderMultiplier { get; set; }
        
        public Mode Mode { get; set; }

        public uint PassCount { get; set; }
        public uint PlayCount { get; set; }

        public uint TotalLength { get; set; }
        public uint HitLength { get; set; }

        public uint CountNormal { get; set; }
        public uint CountSlider { get; set; }
        public uint CountSpinner { get; set; }

        public bool HasStoryboard { get; set; }
        public bool HasVideo { get; set; }
    }
}