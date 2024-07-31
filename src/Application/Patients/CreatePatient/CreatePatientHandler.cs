using HealthMed.Application.Abstractions;
using HealthMed.Domain.PatientAggregate;
using MediatR;

namespace HealthMed.Application.Patients.CreatePatient;

public sealed class CreatePatientHandler : IRequestHandler<CreatePatientCommand, Result<Guid, Error>>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePatientHandler(IPatientRepository patientRepository, IUnitOfWork unitOfWork)
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, Error>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = request.MapToPatient();

        await _patientRepository.Add(patient);

        return await _unitOfWork.Commit(patient.Id, cancellationToken);
    }
}
