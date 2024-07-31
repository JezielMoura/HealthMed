using HealthMed.Domain.SchedulingAggregate;
using MediatR;

namespace HealthMed.Application.Schedulings.CreateScheduling;

public sealed record CreateSchedulingCommand(
    Guid DoctorId,
    Guid PatientId,
    DateTimeOffset Date
) : IRequest<Result<Guid, Error>>
{
    public Scheduling MapToScheduling(Guid availabilityId, string doctorName) =>
        new (Guid.NewGuid(), DoctorId, PatientId, availabilityId, doctorName, Date.UtcDateTime);
}