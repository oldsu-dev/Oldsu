namespace Oldsu.Types
{
    public class Rating
    {
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public int BeatmapID { get; set; }
        public virtual Beatmap Beatmap { get; set; }

        public float Rate { get; set; }
    }
}