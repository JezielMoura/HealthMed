using HealthMed.Domain.DoctorAggregate;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace HealthMed.Application.Doctors.CreateDoctor;

public sealed record CreateDoctorCommand (
    string Name,
    string Email,
    string CPF,
    string CRM,
    string Password
) : IRequest<Result<Guid, Error>>
{
    public Doctor MapToDoctor() =>
        new (Guid.NewGuid(), Name, Email, CPF, CRM, BC.HashPassword(Password));
}
