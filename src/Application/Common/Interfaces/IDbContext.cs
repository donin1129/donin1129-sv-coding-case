using SvCodingCase.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SvCodingCase.Application.Common.Interfaces;

public interface IDbContext
{
    DbSet<Building> Buildings { get; }

    DbSet<Group> Groups { get; }

    DbSet<Lock> Locks { get; }

    DbSet<Media> Medias { get; }

    DbSet<License> Licenses { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
