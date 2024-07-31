using HealthMed.Application.Abstractions;
using HealthMed.Domain.DoctorAggregate;
using MediatR;

namespace HealthMed.Application.Doctors.CreateDoctor;

public sealed class CreateDoctorHandler : IRequestHandler<CreateDoctorCommand, Result<Guid, Error>>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDoctorHandler(IDoctorRepository DoctorRepository, IUnitOfWork unitOfWork)
    {
        _doctorRepository = DoctorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, Error>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = request.MapToDoctor();

        await _doctorRepository.Add(doctor);

        return await _unitOfWork.Commit(doctor.Id, cancellationToken);
    }
}
