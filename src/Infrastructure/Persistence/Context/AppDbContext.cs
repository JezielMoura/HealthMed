using System.Diagnostics.CodeAnalysis;
using HealthMed.Application.Abstractions;
using Nett.Core;

namespace HealthMed.Infrastructure.Persistence.Context;

[ExcludeFromCodeCoverage]
internal sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Availability> Availabilities => Set<Availability>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Scheduling> Schedulings => Set<Scheduling>();

    public async Task<Result<bool, Error>> Commit(CancellationToken cancellationToken = default)
    {
        var isSuccess = await SaveChangesAsync(cancellationToken) > 0;

        if (isSuccess)
            return true;

        return new Error(Errors: [new ("Occour error on save changes")]);
    }

    public async Task<Result<Guid, Error>> Commit(Guid id, CancellationToken cancellationToken = default)
    {
        var isSuccess = await SaveChangesAsync(cancellationToken) > 0;

        if (isSuccess)
            return id;

        return new Error(Errors: [new ("Occour error on save changes")]);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
