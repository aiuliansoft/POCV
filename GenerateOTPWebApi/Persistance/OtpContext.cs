using Microsoft.EntityFrameworkCore;
using Models;

namespace Persistance;

internal class OtpContext : DbContext
{
    public OtpContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Otp> Otp => Set<Otp>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
