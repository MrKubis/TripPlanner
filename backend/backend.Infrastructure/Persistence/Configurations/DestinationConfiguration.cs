using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Configurations;

public class DestinationConfiguration : IEntityTypeConfiguration<Destination>
{
    public void Configure(EntityTypeBuilder<Destination> builder)
    {
        builder.ToTable("destinations");
        builder.HasKey(x => x.Id);
        builder.ComplexProperty(x => x.Location, location =>
        {
            location.Property(l => l.Latitude).HasColumnName("latitude");
            location.Property(l => l.Longitude).HasColumnName("longitude");
        });
        builder.HasMany(x => x.Links)
            .WithOne();
    }
}