using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using HealthMed.Application.Abstractions;
using HealthMed.Application.Availabilities.CreateAvailability;
using HealthMed.Application.Availabilities.UpdateAvailability;
using HealthMed.Domain.AvailabilityAggregate;
using HealthMed.Domain.DoctorAggregate;
using NSubstitute;

namespace HealthMed.Unit.Tests.Availabilities;

[ExcludeFromCodeCoverage]
public sealed class UpdateAvailabilityHandlerTests
{
    private readonly IAvailabilityRepository _availabilityRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UpdateAvailabilityHandler _handler;

    public UpdateAvailabilityHandlerTests()
    {
        _availabilityRepository = Substitute.For<IAvailabilityRepository>();
        _doctorRepository = Substitute.For<IDoctorRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new UpdateAvailabilityHandler(_availabilityRepository, _unitOfWork, _doctorRepository);
    }

    [Fact]
    public async Task Handler_WithDateRange_ShouldAddMultiplesAvailability()
    {
        //Act
        var from = DateTimeOffset.Parse("2024-08-06T08:00:00");
        var to = DateTimeOffset.Parse("2024-08-06T12:00:00");
        var command = new UpdateAvailabilityCommand(Guid.NewGuid(), IsAvailable: true, from, to, 15);

        _doctorRepository.Get(Arg.Any<Guid>()).Returns(new Fixture().Create<Doctor>());

        //Act
        await _handler.Handle(command, default);
    
        //Assert
        await _availabilityRepository.Received(16).Add(Arg.Any<Availability>());
    }
}