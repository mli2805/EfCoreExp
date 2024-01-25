using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace EfCoreExp;

public class MyContext : DbContext
{
    public MyContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public DbSet<AlarmProfileEf> AlarmProfiles { get; set; }
    public DbSet<ThresholdEf> Thresholds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=my.db;Cache=Shared");
    }

    protected override void OnModelCreating(ModelBuilder bulider)
    {
        bulider.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(bulider);
    }
}