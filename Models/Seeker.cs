namespace BloodBank.Models
{
    public class Seeker
    {
        public int Id { get; set; }
      
        public User? User { get; set; }
        public List<Userlist> Donors { get; set; } // Navigation property
    }
}
