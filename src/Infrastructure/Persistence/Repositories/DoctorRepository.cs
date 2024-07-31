using HealthMed.Infrastructure.Persistence.Context;

namespace HealthMed.Infrastructure.Persistence.Repositories;

internal sealed class DoctorRepository(AppDbContext appDbContext) : IDoctorRepository
{
    private readonly DbSet<Doctor> _dbSet = appDbContext.Doctors;
    
    public async Task Add(Doctor doctor)
    {
        await _dbSet.AddAsync(doctor);
    }

    public async Task<Doctor?> Get(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<Doctor?> Get(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }
}