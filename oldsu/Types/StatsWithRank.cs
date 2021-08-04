using Microsoft.EntityFrameworkCore;
using Oldsu.Enums;

namespace Oldsu.Types
{
    [Keyless]
    public class StatsWithRank
    {
        public uint Rank { get; set; }
        
        public int UserID { get; set; }

        public Mode Mode { get; set; }

        public ulong RankedScore { get; set; }
        public ulong TotalScore { get; set; }

        public double UserLevel { get; set; }
        public float Accuracy { get; set; }
        
        public ulong Playcount { get; set; }

        public uint CountSSH { get; set; }
        public uint CountSS { get; set; }
        public uint CountSH { get; set; }
        public uint CountS { get; set; }
        public uint CountA { get; set; }
        public uint CountB { get; set; }
        public uint CountC { get; set; }
        public uint CountD { get; set; }

        public ulong Hit300 { get; set; }
        public ulong Hit100 { get; set; }
        public ulong Hit50 { get; set; }
        public ulong HitMiss { get; set; }
    }
}