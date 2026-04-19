using JobMatch.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JobMatch.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<JobSeeker> JobSeekers { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Match> Matches { get; set; }

    }
}