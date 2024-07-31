using HealthMed.Domain.PatientAggregate;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace HealthMed.Application.Patients.CreatePatient;

public sealed record CreatePatientCommand (
    string Name,
    string Email,
    string CPF,
    string Password
) : IRequest<Result<Guid, Error>>
{
    public Patient MapToPatient() =>
        new (Guid.NewGuid(), Name, Email, CPF, BC.HashPassword(Password, workFactor: 12));
}
