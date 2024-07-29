namespace HealthMed.Domain.AvailabilityAggregate;

public interface IAvailabilityRepository
{
    Task<IEnumerable<Availability>> Get(bool isAvailable = true);
    Task Update(Availability availability);
}