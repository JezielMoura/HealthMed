
namespace HealthMed.Domain.AvailabilityAggregate;

public interface IAvailabilityRepository
{
    Task<Availability?> Get(Guid doctorId, DateTimeOffset date, bool isAvailable);
    Task<IEnumerable<Availability>> Get(int page, int limit, bool isAvailable = true);
    Task Add(Availability availability);
    Task Update(Availability availability);
    Task Delete(Guid doctorId, DateTimeOffset from, DateTimeOffset to);
}