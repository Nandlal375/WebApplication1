using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using DataAccessLayer;

namespace WebApplication1.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Country> countries { get; set; }

        public DbSet<Registration> registrations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
