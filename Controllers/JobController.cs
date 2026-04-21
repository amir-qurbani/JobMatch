using Microsoft.AspNetCore.Mvc;
using JobMatch.API.Data;
using Microsoft.EntityFrameworkCore;
using JobMatch.API.Models;

namespace JobMatch.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly AppDbContext _context;
        public JobController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var jobs = await _context.Jobs.ToListAsync();
            return Ok(jobs);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return Ok(job);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
                return NotFound();

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
                return NotFound();
            return Ok(job);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Job updateJob)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
                return NotFound();

            job.CompanyName = updateJob.CompanyName;
            job.JobName = updateJob.JobName;
            job.Skills = updateJob.Skills;

            await _context.SaveChangesAsync();
            return Ok();
        }
    }

}