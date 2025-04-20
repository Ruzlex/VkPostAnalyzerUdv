using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext: DbContext
{
    public DbSet<Auth> Auth { get; set; }
    public DbSet<LetterCount> LetterCounts { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LetterCount>(entity =>
        {
            entity.HasKey(e => e.Letter);
            entity.Property(e => e.Letter).ValueGeneratedNever();
        });
    }
}