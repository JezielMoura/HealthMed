
namespace HealthMed.Domain.PatientAggregate;

public interface IPatientRepository
{
    Task<Patient?> Get(string email);
    Task<Patient?> Get(Guid patientId);
    Task Add(Patient patient);
}