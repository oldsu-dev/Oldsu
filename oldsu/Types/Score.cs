using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Oldsu.Types
{
    public class ScoreRow
    {
        [Key]
        public uint ScoreId { get; set; }

        public uint UserId { get; set; }
        public virtual UserInfo User { get; set; }

        public string BeatmapHash { get; set; }

        [ForeignKey("BeatmapHash")]
        public virtual Beatmap Beatmap { get; set; }
        
        public string SubmitHash { get; set; }
        
        public ulong Score { get; set; }
        public uint MaxCombo { get; set; }
        public string Grade { get; set; }
        public uint Mods { get; set; }

        public uint Hit300 { get; set; }
        public uint Hit100 { get; set; }
        public uint Hit50 { get; set; }
        public uint HitMiss { get; set; }
        public uint HitGeki { get; set; }
        public uint HitKatu { get; set; }

        public bool Perfect { get; set; }
        public bool Passed { get; set; }
        public bool Ranked { get; set; }
        public byte Gamemode { get; set; }

        public string Version { get; set; }

        public DateTime SubmittedAt { get; set; }
    }
}