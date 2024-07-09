using Microsoft.EntityFrameworkCore;
using Newsletter.Api.Sagas;

namespace Newsletter.Api.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NewsletterOnboardingSagaData>().HasKey(s => s.CorrelationId);
    }

    public DbSet<Subscriber> Subscribers { get; set; }

    public DbSet<NewsletterOnboardingSagaData> SagaData { get; set; }
}
