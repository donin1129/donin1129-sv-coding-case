using SvCodingCase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SvCodingCase.Infrastructure.Persistence.Configurations;

public class BuildingConfiguration : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> builder)
    {
        builder
            .ToTable("Buildings")
            .HasComment("Contains information of a building.");

        builder
            .Property(t => t.Id)
            .IsRequired()
            .HasComment("Unique identifier of a building.");

        builder
            .Property(t => t.Name)
            .IsRequired()
            .HasComment("Name of a building.");

        builder
            .Property(t => t.ShortCut)
            .HasMaxLength(64)  // TODO-ZD: Move to appsettings
            .HasComment("Abbreviation of a building.");

        builder
            .Property(t => t.Description)
            .HasMaxLength(1024)  // TODO-ZD: Move to appsettings
            .HasComment("Description of a building.");

        builder
            .HasIndex(t => t.Id);

        builder
            .HasKey(t => t.Id);

        builder
            .HasIndex(t => t.Name);

        builder
            .HasIndex(t => t.ShortCut);
    }
}
