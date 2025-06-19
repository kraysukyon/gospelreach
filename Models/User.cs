namespace GospelReachCapstone.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; } // Store hashed passwords!
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
