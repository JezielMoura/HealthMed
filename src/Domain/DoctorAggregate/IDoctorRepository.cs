namespace HealthMed.Domain.DoctorAggregate;

public interface IDoctorRepository
{
    Task Add(Doctor doctor);
}