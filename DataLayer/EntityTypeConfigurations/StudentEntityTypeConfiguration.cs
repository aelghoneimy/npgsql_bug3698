using System.Text.Json;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntityTypeConfigurations;

public class StudentEntityTypeConfiguration : IEntityTypeConfiguration<StudentEntity>
{
    public const string Table = "Student";

    public void Configure(EntityTypeBuilder<StudentEntity> builder)
    {
        builder.ToTable(Table, AppDbContext.Schema);

        builder.Property(x => x.Id).HasColumnOrder(0);

        builder.Property(x => x.Document)
            .HasColumnType("jsonb")
            .IsRequired();

        builder.HasData(
            
        );
    }
}