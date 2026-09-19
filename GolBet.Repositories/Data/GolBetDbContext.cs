using GolBet.Entities;
using GolBet.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace GolBet.Repositories.Data;

public class GolBetDbContext : DbContext
{
    public GolBetDbContext(DbContextOptions<GolBetDbContext> options) : base(options)
    {
    }

    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<Bet> Bets => Set<Bet>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>(entity =>
        {
            entity.Property(t => t.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.HasIndex(t => t.Name).IsUnique();
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.Property(m => m.HomeOdds).HasColumnType("decimal(5,2)");
            entity.Property(m => m.DrawOdds).HasColumnType("decimal(5,2)");
            entity.Property(m => m.AwayOdds).HasColumnType("decimal(5,2)");

            entity.HasOne(m => m.HomeTeam)
                  .WithMany(t => t.HomeMatches)
                  .HasForeignKey(m => m.HomeTeamId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(m => m.AwayTeam)
                  .WithMany(t => t.AwayMatches)
                  .HasForeignKey(m => m.AwayTeamId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Bet>(entity =>
        {
            entity.Property(b => b.Amount).HasColumnType("decimal(10,2)");
            entity.Property(b => b.OddsAtBetTime).HasColumnType("decimal(5,2)");

            entity.HasOne(b => b.Match)
                  .WithMany(m => m.Bets)
                  .HasForeignKey(b => b.MatchId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        StampAuditFields();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        StampAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void StampAuditFields()
    {
        var now = DateTime.Now;

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = now;
            }
        }
    }
}
