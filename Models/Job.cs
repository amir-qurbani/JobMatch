namespace JobMatch.API.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string JobName { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
    }
}