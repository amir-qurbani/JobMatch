using Microsoft.AspNetCore.Mvc;
using JobMatch.API.Data;
using Microsoft.EntityFrameworkCore;
using JobMatch.API.Models;

namespace JobMatch.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobSeekerController : ControllerBase
    {
        private readonly AppDbContext _context;
        public JobSeekerController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var jobSekkers = await _context.JobSeekers.ToListAsync();
            return Ok(jobSekkers);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] JobSeeker jobSeeker)
        {
            _context.JobSeekers.Add(jobSeeker);  // Lägg till
            await _context.SaveChangesAsync();    // Spara
            return Ok(jobSeeker);                 // Returnera

        }

    }
}