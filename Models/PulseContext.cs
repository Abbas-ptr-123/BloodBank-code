using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Models
{
    public class PulseContext : IdentityDbContext<User>
    {
        public PulseContext(DbContextOptions<PulseContext> options) : base(options) { }

        public DbSet<Donor> Donors { get; set; }
        public DbSet<Seeker> Seekers { get; set; }
        public DbSet<ForgotPassword> ForgotPasswords { get; set; }
        public DbSet<ResetPassword> ResetPasswords { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Define additional model configurations here
        }
    }
}
