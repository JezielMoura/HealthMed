namespace HealthMed.Domain.DoctorAggregate;

public interface IDoctorRepository
{
    Task<Doctor?> Get(string email);
    Task Add(Doctor doctor);
}