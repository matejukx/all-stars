using AllStars.Domain.Dutch.Models;
using AllStars.Domain.User.Models;
using Microsoft.EntityFrameworkCore;

namespace AllStars.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<DutchScore> DutchScores { get; set; }
    public DbSet<DutchGame> DutchGames { get; set; }
    public DbSet<AllStarUser> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DutchGame>(entity =>
        {
            entity.HasKey(g => g.Id);

            entity.HasMany(g => g.DutchScores)
                  .WithOne(s => s.Game)
                  .HasForeignKey(s => s.DutchGameId);
        });

        modelBuilder.Entity<DutchScore>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.HasOne(s => s.Game)
                  .WithMany(g => g.DutchScores)
                  .HasForeignKey(s => s.DutchGameId);

            entity.HasOne(s => s.Player)
                  .WithMany(g => g.DutchScores)
                  .HasForeignKey(s => s.PlayerId);
        });

        modelBuilder.Entity<AllStarUser>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasMany(g => g.DutchScores)
                  .WithOne(s => s.Player)
                  .HasForeignKey(s => s.PlayerId);
        });
    }
}