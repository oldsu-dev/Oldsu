namespace Oldsu.Types
{
    public class Beatmapset
    {
        public int BeatmapsetID { get; set; }

        public uint? CreatorID { get; set; }

        public string Title { get; set; }

        public byte RankingStatus { get; set; }
    }
}