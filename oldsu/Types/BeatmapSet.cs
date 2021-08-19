namespace Oldsu.Types
{
    public class Beatmapset
    {
        public int BeatmapsetID { get; set; }

        public uint? CreatorID { get; set; }
        public string CreatorName { get; set; }

        public string Title { get; set; }
        public string Artist { get; set; }

        public byte RankingStatus { get; set; }

        public float Rating { get; set; }
    }
}