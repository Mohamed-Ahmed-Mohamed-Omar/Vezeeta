using Microsoft.EntityFrameworkCore;
using Vezeeta.Data.Entities;

namespace Vezeeta.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        public DbSet<Contact_us> contact_Us { get; set; }
        public DbSet<Area> areas { get; set; }
        public DbSet<Gender> genders { get; set; }
        public DbSet<Patient> patients { get; set; }
    }
}
