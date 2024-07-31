using HealthMed.Application.Abstractions;
using HealthMed.Domain.AvailabilityAggregate;
using HealthMed.Domain.DoctorAggregate;
using MediatR;

namespace HealthMed.Application.Availabilities.CreateAvailability;

public sealed class CreateAvailabilityHandler : IRequestHandler<CreateAvailabilityCommand, Result<bool, Error>>
{
    private readonly IAvailabilityRepository _availabilityRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAvailabilityHandler(
        IAvailabilityRepository availabilityRepository, 
        IUnitOfWork unitOfWork, 
        IDoctorRepository doctorRepository)
    {
        _availabilityRepository = availabilityRepository;
        _doctorRepository = doctorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool, Error>> Handle(CreateAvailabilityCommand command, CancellationToken cancellationToken)
    {
        var totalMinutes = (command.To - command.From).TotalMinutes;
        var totalAvailability = totalMinutes / command.DurationInMinutes;
        var doctor = await _doctorRepository.Get(command.DoctorId);

        if (doctor is null)
            return new Error(Errors: [ new("Doctor not found") ]);

        for (int i = 0; i < totalAvailability; i++)
        {
            var availabilityDatetime = command.From.AddMinutes(command.DurationInMinutes * i).UtcDateTime;
            var availability = new Availability(Guid.NewGuid(), command.DoctorId, doctor.Name, command.IsAvailable, availabilityDatetime);

            await _availabilityRepository.Add(availability);
        }

        return await _unitOfWork.Commit(cancellationToken);
    }
}
