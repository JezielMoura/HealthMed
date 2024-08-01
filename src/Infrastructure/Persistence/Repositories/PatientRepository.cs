using System.Diagnostics.CodeAnalysis;
using HealthMed.Infrastructure.Persistence.Context;

namespace HealthMed.Infrastructure.Persistence.Repositories;

[ExcludeFromCodeCoverage]
internal sealed class PatientRepository(AppDbContext appDbContext) : IPatientRepository
{
    private readonly DbSet<Patient> _dbSet = appDbContext.Patients;
    
    public async Task<Patient?> Get(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task Add(Patient patient)
    {
        await _dbSet.AddAsync(patient);
    }

    public async Task<Patient?> Get(Guid patientId)
    {
        return await _dbSet.FindAsync(patientId);
    }
}