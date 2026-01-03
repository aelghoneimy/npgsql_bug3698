using System.Text.Json;
using DataLayer.Entities;
using DataLayer.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public const string Schema = "bug3698";

    public virtual DbSet<StudentEntity> Students { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.ApplyConfiguration(new StudentEntityTypeConfiguration());
    }
}