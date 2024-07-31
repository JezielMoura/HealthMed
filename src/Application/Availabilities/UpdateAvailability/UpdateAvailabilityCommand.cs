using MediatR;

namespace HealthMed.Application.Availabilities.CreateAvailability;

public sealed record UpdateAvailabilityCommand(
    Guid DoctorId,
    bool IsAvailable,
    DateTimeOffset From,
    DateTimeOffset To,
    int DurationInMinutes
) : IRequest<Result<bool, Error>>;