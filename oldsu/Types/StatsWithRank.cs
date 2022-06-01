using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Oldsu.Enums;

namespace Oldsu.Types
{
    [NotMapped]
    public class StatsWithRank : Stats
    {
        public uint Rank { get; set; }
    }
}