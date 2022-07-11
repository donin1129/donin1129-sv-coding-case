using SvCodingCase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SvCodingCase.Infrastructure.Persistence.Configurations;

public class LicenseConfiguration : IEntityTypeConfiguration<License>
{
    public void Configure(EntityTypeBuilder<License> builder)
    {
        builder
            .ToTable("License")
            .HasComment("Contains information of a license.");

        builder
            .Property(t => t.UserId)
            .IsRequired()
            .HasComment("Unique identifier of a user.");

        builder
            .Property(t => t.LatestLicense)
            .IsRequired()
            .HasComment("The latest license info of the user.");

        builder
            .HasIndex(t => t.UserId);

        builder
            .HasKey(t => t.UserId);
    }
}
