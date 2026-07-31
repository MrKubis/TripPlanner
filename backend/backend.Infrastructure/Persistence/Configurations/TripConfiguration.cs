using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Configurations;

public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.ToTable("trips");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(256)
            .HasDefaultValue(string.Empty);
        builder.Property(t => t.Description)
            .HasDefaultValue(string.Empty);
        
        builder.HasMany(t => t.Days)
            .WithOne(d => d.Trip)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(t => t.Expenses)
            .WithOne(e => e.Trip)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(t => t.Links)
            .WithOne(l => l.Trip)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(t => t.Days)
            .WithOne(d => d.Trip)
            .OnDelete(DeleteBehavior.Cascade);
    }
}