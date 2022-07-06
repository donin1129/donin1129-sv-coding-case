using SvCodingCase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SvCodingCase.Infrastructure.Persistence.Configurations;

public class LockConfiguration : IEntityTypeConfiguration<Lock>
{
    public void Configure(EntityTypeBuilder<Lock> builder)
    {
        builder
            .ToTable("Locks")
            .HasComment("Contains information of a lock.");

        builder
            .Property(t => t.Id)
            .IsRequired()
            .HasComment("Unique identifier of a lock.");

        builder
            .Property(t => t.Description)
            .HasMaxLength(1024)  // TODO-ZD: Move to appsettings
            .HasComment("Description of a lock.");

        builder
            .Property(t => t.SerialNumber)
            .IsRequired()
            .HasComment("Serial number of a lock.");

        builder
            .HasOne(t => t.Building)
            .WithMany(b => b.Locks)
            .HasForeignKey(t => t.BuildingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(t => t.Type)
            .IsRequired()
            .HasComment("Type of a lock.");

        builder
            .Property(t => t.Name)
            .IsRequired()
            .HasComment("Name of a lock.");

        builder
            .Property(t => t.Floor)
            .HasComment("The floor level a lock is located.");

        builder
            .Property(t => t.RoomNumber)
            .HasComment("The room number a lock is located.");

        builder
            .HasIndex(t => t.Id);

        builder
            .HasKey(t => t.Id);

        builder
            .HasIndex(t => t.Name);

        builder
            .HasIndex(t => t.Type);

        builder
            .HasIndex(t => t.SerialNumber);
    }
}
