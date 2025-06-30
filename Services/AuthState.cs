namespace GospelReachCapstone.Services
{
    public class AuthState
    {
        public bool IsLoggedIn { get; set; }
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? DisplayName { get; set; }
        public string? Role { get; set; }
    }
}
