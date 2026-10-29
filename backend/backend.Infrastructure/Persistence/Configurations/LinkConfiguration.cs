using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Configurations;

public class LinkConfiguration : IEntityTypeConfiguration<Link>
{
    public void Configure(EntityTypeBuilder<Link> builder)
    {
        builder.ToTable("links");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Url)
            .IsRequired()
            .HasMaxLength(256);
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(256)
            .HasDefaultValue(string.Empty);
    }
}