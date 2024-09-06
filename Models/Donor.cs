namespace BloodBank.Models
{
    public class Donor
    {
        public int Id { get; set; }
       
        public User? User { get; set; }
        public List<Userlist> Seekers { get; set; } // Navigation property
    }
}
