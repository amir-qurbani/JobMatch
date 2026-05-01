
using JobMatch.API.Models;
using JobMatch.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobMatch.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MatchController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var matches = await _context.Matches.ToListAsync();
            return Ok(matches);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Match match)
        {
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();
            return Ok(match);
        }
        [HttpPost("calculate/{jobSeekerId}/{jobId}")]
        public async Task<ActionResult> CreateMatch(int jobSeekerId, int jobId)
        {
            var jobSeeker = await _context.JobSeekers.FindAsync(jobSeekerId);
            if (jobSeeker == null)
            {
                return NotFound("Jobbsökaren hittades inte");
            }
            var job = await _context.Jobs.FindAsync(jobId);
            if (job == null)
            {
                return NotFound("Jobbet hittades inte");
            }
            var seekerSkills = jobSeeker.Skills
                .Split(',')
                .Select(s => s.Trim().ToLower())
                .ToList();

            var jobSkills = job.Skills
                .Split(',')
                .Select(s => s.Trim().ToLower())
                .ToList();

            var commonSkills = seekerSkills.Intersect(jobSkills).Count();
            var matchPercent = (int)((double)commonSkills / jobSkills.Count * 100);

            var match = new Match
            {
                JobSeekerId = jobSeekerId,
                JobId = jobId,
                MatchPercent = matchPercent
            };
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();
            return Ok(match);

        }
    }

}