using System.ComponentModel.DataAnnotations;
using JobMatch.API.Data;
using JobMatch.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobMatch.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto dto)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new User
            {
                Email = dto.Email,
                PasswordHash = passwordHash
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("Användare skapad!");
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] RegisterDto dto)
        {
            //Hitta användaren
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
            {
                return Unauthorized("Fel email eller lösenord");
            }
            var passwordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!passwordValid)
                return Unauthorized("Fel email eller lösenord");

            return Ok("Inloggning lyckades!");  // ← LÄGG TILL DENNA
        }
    }

}