using HealthMed.Infrastructure.Persistence.Context;

namespace HealthMed.Infrastructure.Persistence.Repositories;

internal sealed class AvailabilityRepository (AppDbContext appDbContext) : IAvailabilityRepository
{
    private readonly DbSet<Availability> _dbSet = appDbContext.Availabilities;

    public async Task<IEnumerable<Availability>> Get(bool isAvailable = true)
    {
        return await _dbSet.Where(x => x.IsAvailable == isAvailable).ToListAsync();
    }

    public async Task Update(Availability availability)
    {
        await Task.FromResult(_dbSet.Update(availability));
    }
}