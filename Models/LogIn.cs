namespace BloodBank.Models
{
    public class LogIn
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
