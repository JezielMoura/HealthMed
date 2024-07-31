using HealthMed.Application.Abstractions;
using HealthMed.Domain.AvailabilityAggregate;
using HealthMed.Domain.DoctorAggregate;
using HealthMed.Domain.PatientAggregate;
using HealthMed.Domain.SchedulingAggregate;
using MediatR;

namespace HealthMed.Application.Schedulings.CreateScheduling;

public sealed class CreateSchedulingHandler : IRequestHandler<CreateSchedulingCommand, Result<Guid, Error>>
{
    private readonly ISchedulingRepository _schedulingRepository;
    private readonly IAvailabilityRepository _availabilityRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMailService _mailService;

    public CreateSchedulingHandler(
        ISchedulingRepository schedulingRepository,
        IAvailabilityRepository availabilityRepository,
        IDoctorRepository doctorRepository,
        IUnitOfWork unitOfWork,
        IMailService mailService,
        IPatientRepository patientRepository)
    {
        _schedulingRepository = schedulingRepository;
        _availabilityRepository = availabilityRepository;
        _doctorRepository = doctorRepository;
        _unitOfWork = unitOfWork;
        _mailService = mailService;
        _patientRepository = patientRepository;
    }

    public async Task<Result<Guid, Error>> Handle(CreateSchedulingCommand command, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.Get(command.DoctorId);
        var patient = await _patientRepository.Get(command.PatientId);
        var availability = await _availabilityRepository.Get(command.DoctorId, command.Date.UtcDateTime, isAvailable: true);

        if (doctor is null)
            return new Error(Errors: [new("Doctor not found")]);

        if (patient is null)
            return new Error(Errors: [new("Patient not found")]);

        if (availability is null)
            return new Error(Errors: [new("Date no available")]);

        var scheduling = command.MapToScheduling(availability.Id, doctor.Name);

        availability.IsUnavailable();

        await _availabilityRepository.Update(availability);
        await _schedulingRepository.Add(scheduling);
        await _mailService.Send(doctor.Email, doctor.Name, patient.Name, command.Date);

        return await _unitOfWork.Commit(scheduling.Id, cancellationToken);
    }
}
