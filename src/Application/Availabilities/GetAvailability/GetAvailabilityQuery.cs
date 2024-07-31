using MediatR;

namespace HealthMed.Application.Availabilities.GetAvailability;

public record GetAvailabilityQuery(int Page = 1, int Limit = 10) : IRequest<IEnumerable<AvailabilityResponse>>;