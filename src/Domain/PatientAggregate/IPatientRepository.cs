namespace HealthMed.Domain.PatientAggregate;

public interface IPatientRepository
{
    Task Add(Patient patient);
}