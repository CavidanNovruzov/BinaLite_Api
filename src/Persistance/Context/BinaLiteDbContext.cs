using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistance.Context;

public class BinaLiteDbContext:DbContext
{
    public BinaLiteDbContext(DbContextOptions<BinaLiteDbContext> options):base(options)
    {
    }
    public DbSet<PropertyAd> PropertyAds { get; set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
