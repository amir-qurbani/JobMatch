namespace JobMatch.API.Models
{
    public class Match
    {
        public int Id { get; set; }
        public int JobSeekerId { get; set; }
        public int JobId { get; set; }
        public int MatchPercent { get; set; }

    }
}