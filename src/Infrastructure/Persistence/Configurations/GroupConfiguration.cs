using SvCodingCase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SvCodingCase.Infrastructure.Persistence.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder
            .ToTable("Groups")
            .HasComment("Contains information of a group.");

        builder
            .Property(t => t.Id)
            .IsRequired()
            .HasComment("Unique identifier of a group.");

        builder
            .Property(t => t.Description)
            .HasMaxLength(1024)  // TODO-ZD: Move to appsettings
            .HasComment("Description of a group.");

        builder
            .HasIndex(t => t.Id);

        builder
            .HasKey(t => t.Id);

        builder
            .HasIndex(t => t.Name);
    }
}
