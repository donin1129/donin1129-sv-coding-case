using SvCodingCase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SvCodingCase.Infrastructure.Persistence.Configurations;

public class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder
            .ToTable("Media")
            .HasComment("Contains information of a media.");

        builder
            .Property(t => t.Id)
            .IsRequired()
            .HasComment("Unique identifier of a media.");

        builder
            .Property(t => t.Description)
            .HasMaxLength(1024)  // TODO-ZD: Move to appsettings
            .HasComment("Description of a media.");

        builder
            .Property(t => t.SerialNumber)
            .IsRequired()
            .HasComment("Serial number of a media.");

        builder
            .HasOne(t => t.Group)
            .WithMany(g => g.Media)
            .HasForeignKey(t => t.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(t => t.Type)
            .IsRequired()
            .HasComment("Type of a media.");

        builder
            .Property(t => t.Owner)
            .IsRequired()
            .HasComment("Owner of a media.");

        builder
            .HasIndex(t => t.Id);

        builder
            .HasKey(t => t.Id);

        builder
            .HasIndex(t => t.Owner);

        builder
            .HasIndex(t => t.Type);

        builder
            .HasIndex(t => t.SerialNumber);
    }
}
