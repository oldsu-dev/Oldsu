using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oldsu.Types;

namespace Oldsu.Utils
{
    public static class TypeExtensions
    {
        public static void UpdateStats(this Stats stats, ScoreRow score)
        {
            stats.TotalScore += score.Score;
            stats.Playcount++;

            if (score.Passed)
            {
                stats.Hit300 += score.Hit300;
                stats.Hit100 += score.Hit100;
                stats.Hit50 += score.Hit50;
                stats.HitMiss += score.HitMiss;
            
            
                stats.Accuracy = (float)Math.Round(
                    (double)(stats.Hit50 * 50 + stats.Hit100 * 100 + stats.Hit300 * 300) / (double)((stats.Hit300 + stats.Hit100 + stats.Hit50) * 300) * 100,
                    2);
            }
        }

        public static async Task<ScoreRow?> SerializeScoreString(string[] values, uint userId)
        {
            try
            {
                var score = new ScoreRow
                {
                    // mmm only username is there
                    UserId = userId,
                    BeatmapHash = values[0],
                    SubmitHash = values[2],
                    Hit300 = uint.Parse(values[3]),
                    Hit100 = uint.Parse(values[4]),
                    Hit50 = uint.Parse(values[5]),
                    HitGeki = uint.Parse(values[6]),
                    HitKatu = uint.Parse(values[7]),
                    HitMiss = uint.Parse(values[8]),
                    Score = ulong.Parse(values[9]),
                    MaxCombo = ushort.Parse(values[10]),
                    Perfect = values[11] == "True",
                    Grade = values[12],
                    Mods = ushort.Parse(values[13]),
                    Passed = values[14] == "True",
                    Gamemode = byte.Parse(values[15]),
                    SubmittedAt = DateTime.ParseExact(values[16], "yyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
                };
                
                return score;
            }
            catch (IndexOutOfRangeException ex)
            {
                // logger
                Console.WriteLine(ex);
            }

            return null;
        }

        public static async Task<StatsWithRank> CreateStats(uint userId)
        {
            throw new NotImplementedException();
        }
    }
}