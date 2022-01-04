using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Oldsu.Enums;

namespace Oldsu.Types
{
    public class RankHistory
    {
        [JsonIgnore]
        [Key] public uint RankHistoryID { get; set; }

        [JsonIgnore]
        public uint UserID { get; set; }
        
        public uint Rank { get; set; }
        public DateTime Date { get; set; }
        public Mode Mode { get; set; }
    }
}