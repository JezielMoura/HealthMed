using System.Diagnostics.CodeAnalysis;
using HealthMed.Infrastructure.Persistence.Context;

namespace HealthMed.Infrastructure.Persistence.Repositories;

[ExcludeFromCodeCoverage]
internal sealed class AvailabilityRepository (AppDbContext appDbContext) : IAvailabilityRepository
{
    private readonly DbSet<Availability> _dbSet = appDbContext.Availabilities;

    public async Task Add(Availability availability)
    {
        await _dbSet.AddAsync(availability);
    }

    public async Task<IEnumerable<Availability>> Get(int page, int limit, bool isAvailable = true)
    {
        var skip = (page - 1) * limit;

        return await _dbSet.Where(x => x.IsAvailable == isAvailable).Skip(skip).Take(limit).ToListAsync();
    }

    public async Task Update(Availability availability)
    {
        await Task.FromResult(_dbSet.Update(availability));
    }

    public async Task Delete(Guid doctorId, DateTimeOffset from, DateTimeOffset to)
    {
        var fromUtc = from.UtcDateTime;
        var toUtc = to.UtcDateTime;
        var availabilitiesToRemove = await _dbSet.Where(x => x.DoctorId == doctorId && x.DateTime >= fromUtc && x.DateTime <= toUtc).ToListAsync();

        _dbSet.RemoveRange(availabilitiesToRemove);
    }

    public async Task<Availability?> Get(Guid doctorId, DateTimeOffset date, bool isAvailable)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.DoctorId == doctorId && x.DateTime == date && x.IsAvailable == isAvailable);
    }
}