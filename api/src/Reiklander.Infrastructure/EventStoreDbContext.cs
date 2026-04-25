using Microsoft.EntityFrameworkCore;

namespace Reiklander.Infrastructure;

public class EventStoreDbContext(DbContextOptions<EventStoreDbContext> options) : DbContext(options)
{
    public DbSet<EventEntity> Events => Set<EventEntity>();
    public DbSet<CharacterReadModel> Characters => Set<CharacterReadModel>();

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

        modelBuilder.Entity<CharacterReadModel>(e =>
                {
                    e.OwnsOne(c => c.WeaponSkill);
                    e.OwnsOne(c => c.BallisticSkill);
                    e.OwnsOne(c => c.Strength);
                    e.OwnsOne(c => c.Toughness);
                    e.OwnsOne(c => c.Initiative);
                    e.OwnsOne(c => c.Agility);
                    e.OwnsOne(c => c.Dexterity);
                    e.OwnsOne(c => c.Intelligence);
                    e.OwnsOne(c => c.Willpower);
                    e.OwnsOne(c => c.Fellowship);
                });
    }
}
