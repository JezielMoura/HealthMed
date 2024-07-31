using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using HealthMed.Application.Abstractions;
using HealthMed.Application.Schedulings.CreateScheduling;
using HealthMed.Domain.AvailabilityAggregate;
using HealthMed.Domain.DoctorAggregate;
using HealthMed.Domain.PatientAggregate;
using HealthMed.Domain.SchedulingAggregate;
using NSubstitute;

namespace HealthMed.Unit.Tests.Schedulings;

[ExcludeFromCodeCoverage]
public sealed class CreateSchedulingHandlerTests
{
    private readonly ISchedulingRepository _schedulingRepository;
    private readonly IAvailabilityRepository _availabilityRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMailService _mailService;
    private readonly CreateSchedulingHandler _handler;

    public CreateSchedulingHandlerTests()
    {
        _schedulingRepository = Substitute.For<ISchedulingRepository>();
        _availabilityRepository = Substitute.For<IAvailabilityRepository>();
        _doctorRepository = Substitute.For<IDoctorRepository>();
        _patientRepository = Substitute.For<IPatientRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _mailService = Substitute.For<IMailService>();
        _handler = new CreateSchedulingHandler(_schedulingRepository, _availabilityRepository, _doctorRepository, _unitOfWork, _mailService, _patientRepository);
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldUpdateAvailabilityAddSchedulingAndSendMail()
    {
        //Arrange
        var command = new Fixture().Create<CreateSchedulingCommand>();

        _doctorRepository.Get(command.DoctorId).Returns(new Fixture().Create<Doctor>());
        _patientRepository.Get(command.PatientId).Returns(new Fixture().Create<Patient>());
        _availabilityRepository.Get(command.DoctorId, command.Date, isAvailable: true).Returns(new Fixture().Create<Availability>());
    
        //Act
        await _handler.Handle(command, default);
    
        //Assert
        await _availabilityRepository.Received(1).Update(Arg.Any<Availability>());
        await _schedulingRepository.Received(1).Add(Arg.Any<Scheduling>());
        await _mailService.Received(1).Send(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<DateTimeOffset>());
    }
}
