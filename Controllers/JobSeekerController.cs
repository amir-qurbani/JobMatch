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
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var jobSeeker = await _context.JobSeekers.FindAsync(id);
            if (jobSeeker == null)
                return NotFound();

            _context.JobSeekers.Remove(jobSeeker);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var jobSeeker = await _context.JobSeekers.FindAsync(id);
            if (jobSeeker == null)
                return NotFound();

            return Ok(jobSeeker);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] JobSeeker updatejobSeeker)
        {
            var jobSeeker = await _context.JobSeekers.FindAsync(id);
            if (jobSeeker == null)
                return NotFound();

            jobSeeker.FullName = updatejobSeeker.FullName;
            jobSeeker.Email = updatejobSeeker.Email;
            jobSeeker.Skills = updatejobSeeker.Skills;

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}