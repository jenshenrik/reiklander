using Microsoft.EntityFrameworkCore;

namespace Reiklander.Infrastructure;

public class EventStoreDbContext : DbContext
{
    public DbSet<EventEntity> Events => Set<EventEntity>();

    public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventEntity>(e =>
        {
            e.HasKey(x => x.Id);

            e.HasIndex(x => x.AggregateId);

            e.HasIndex(x => new { x.AggregateId, x.Version })
                .IsUnique();

            e.Property(x => x.Data).HasColumnType("jsonb");
        });
    }
}
