namespace JobMatch.API.Models
{
    public class RegisterDto
    {
        //DTO - används bara för att ta emot data från användaren
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}