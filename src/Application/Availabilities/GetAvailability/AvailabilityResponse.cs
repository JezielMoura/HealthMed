using HealthMed.Domain.AvailabilityAggregate;

namespace HealthMed.Application.Availabilities.GetAvailability;

public record AvailabilityResponse(Guid DoctorId, string DoctorName, IEnumerable<DateTimeOffset> Availabilities)
{
    public static AvailabilityResponse Create(IGrouping<Guid, Availability> group) =>
        new (group.Key, group.First().DoctorName, group.Select(x => x.DateTime.ToLocalTime()).OrderBy(x => x));
}