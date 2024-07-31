namespace HealthMed.Domain.PatientAggregate;

public interface IPatientRepository
{
    Task<Patient?> Get(string email);
    Task Add(Patient patient);
}