using System;
using Microsoft.EntityFrameworkCore;

namespace Oldsu.Types
{
    [Keyless]
    public class HighScoreWithRank
    {
        public uint ScoreId { get; set; }

        public uint UserId { get; set; }
        public virtual UserInfo UserInfo { get; set; }

        public ulong Rank { get; set; }

        public string BeatmapHash { get; set; }

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

        public DateTime SubmittedAt { get; set; }

        /// <summary>
        ///     Used for GetScores endpoint to convert the Score object into a string.
        /// </summary>
        /// <returns>A osu-friendly format of a score</returns>
        public override string ToString() =>
            $"{ScoreId}|{UserInfo.Username}|{Score}|{MaxCombo}|{Hit50}|{Hit100}|" +
            $"{Hit300}|{HitMiss}|{HitKatu}|{HitGeki}|{(Perfect ? 1 : 0)}|{Mods}|" +
            $"{UserId}|{Rank}|1\n";
    }
}