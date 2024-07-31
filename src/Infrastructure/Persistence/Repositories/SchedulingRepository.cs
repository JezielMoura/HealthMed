using HealthMed.Infrastructure.Persistence.Context;

namespace HealthMed.Infrastructure.Persistence.Repositories;

internal sealed class SchedulingRepository(AppDbContext appDbContext) : ISchedulingRepository
{
    private readonly DbSet<Scheduling> _dbSet = appDbContext.Schedulings;
    
    public async Task Add(Scheduling scheduling)
    {
        await _dbSet.AddAsync(scheduling);
    }
}