

using IronGridConsumer.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace IronGridConsumer.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Units> Units { get; set; }
    public DbSet<Assets> Assets { get; set; }
    public DbSet<AssetLiveStatus> AssetLiveStatus { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Units>() //actually need to think about this
            .HasMany(e => e.Assets)
            .WithOne(e => e.Units)
            .HasForeignKey(e=>e.UnitId)
            .OnDelete(DeleteBehavior.Cascade); //need to think on this

        modelBuilder.Entity<AssetLiveStatus>()
           .HasOne(e => e.Assets)
           .WithOne(e => e.AssetLiveStatus)
           .HasForeignKey<AssetLiveStatus>(e=>e.AssetId)
           .OnDelete(DeleteBehavior.Cascade); //need to think on this


    }
}