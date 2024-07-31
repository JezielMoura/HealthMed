using HealthMed.Application.Abstractions;
using HealthMed.Domain.DoctorAggregate;
using HealthMed.Domain.PatientAggregate;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace HealthMed.Application.Auth;

public sealed class LoginHandler : IRequestHandler<LoginCommand, Result<string, Error>>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly ITokenProvider _tokenProvider;

    public LoginHandler(IDoctorRepository doctorRepository, IPatientRepository patientRepository, ITokenProvider tokenProvider)
    {
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
        _tokenProvider = tokenProvider;
    }

    public async Task<Result<string, Error>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        if (command.IsDoctor)
        {
            if (await _doctorRepository.Get(command.Email) is {} doctor)
            {
                if (BC.Verify(command.Password, doctor.Password))
                    return await _tokenProvider.Create(doctor.Name, "Doctor");
            }

            return new Error(Errors: [ new("E-mail or password incorrect") ]);
        }

        if(await _patientRepository.Get(command.Email) is {} patient)
        {
            if (BC.Verify(command.Password, patient.Password))
                return await _tokenProvider.Create(patient.Name, "Patient");
        }

        return new Error(Errors: [ new("E-mail or password incorrect") ]);
    }
}