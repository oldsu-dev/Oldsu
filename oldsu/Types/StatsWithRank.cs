using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Oldsu.Enums;

namespace Oldsu.Types
{
    [Keyless]
    public class StatsWithRank
    {
        public uint Rank { get; set; }
        
        public uint UserID { get; set; }
        
        public virtual UserInfo User { get; set; }

        [Required]
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

        public Stats ToStats()
        {
            return new Stats
            {
                Accuracy = Accuracy,
                Hit50 = Hit50,
                Hit100 = Hit100,
                Hit300 = Hit300,
                Mode = Mode,
                Playcount = Playcount,
                User = User,
                CountA = CountA,
                CountB = CountB,
                CountC = CountC,
                CountD = CountD,
                CountS = CountS,
                HitMiss = HitMiss,
                RankedScore = RankedScore,
                TotalScore = TotalScore,
                UserLevel = UserLevel,
                CountSH = CountSH,
                CountSS = CountSS,
                UserID = UserID,
                CountSSH = CountSSH
            };
        }
    }
}