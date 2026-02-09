using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CalculatorDomainDemo.Domain;

namespace API.Data
{
    public class CalculatorDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public CalculatorDbContext(DbContextOptions<CalculatorDbContext> options)
            : base(options)
        {
        }

        public DbSet<Calculation> Calculations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Calculation>().HasKey(c => c.Id);
        }
    }
}