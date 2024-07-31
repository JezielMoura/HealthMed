using HealthMed.Domain.AvailabilityAggregate;

namespace HealthMed.Application.Availabilities.GetAvailability;

public record AvailabilityResponse(string DoctorName, IEnumerable<DateTimeOffset> Availabilities)
{
    public static AvailabilityResponse Create(IGrouping<string, Availability> group) =>
        new (group.Key, group.Select(x => x.DateTime.ToLocalTime()).OrderBy(x => x));
}