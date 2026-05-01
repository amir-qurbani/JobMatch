namespace JobMatch.API.Models
{
    public class Match
    {
        public int Id { get; set; }

        public int JobSeekerId { get; set; }        // FK — siffran
        public JobSeeker? JobSeeker { get; set; }   // Navigation — objektet

        public int JobId { get; set; }              // FK — siffran
        public Job? Job { get; set; }               // Navigation — objektet

        public int MatchPercent { get; set; }
    }
}