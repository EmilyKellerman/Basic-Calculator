using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
public class CalculatorDbContext : DbContext
{
    public CalculatorDbContext(DbContextOptions<CalculatorDbContext> options)
    :base(options)
    {
    
    }
    public DbSet<Calculation> Calculations { get; set; }

    protected override void OnModeCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calculations>().HasKey(c => c.Id);
    }

}