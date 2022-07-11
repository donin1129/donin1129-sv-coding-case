using System.Reflection;
using SvCodingCase.Application.Common.Interfaces;
using SvCodingCase.Domain.Entities;
using SvCodingCase.Infrastructure.Identity;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SvCodingCase.Enums;
using Npgsql;

namespace SvCodingCase.Infrastructure.Persistence;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IDbContext
{

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        IMediator mediator) 
        : base(options, operationalStoreOptions)
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<LockType>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<MediaType>();
    }

    public DbSet<Building> Buildings => Set<Building>();

    public DbSet<Group> Groups => Set<Group>();

    public DbSet<Lock> Locks => Set<Lock>();

    public DbSet<Media> Medias => Set<Media>();

    public DbSet<License> Licenses => Set<License>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresEnum<LockType>();
        builder.HasPostgresEnum<MediaType>();

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
