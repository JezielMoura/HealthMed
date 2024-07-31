using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using HealthMed.Application.Abstractions;
using HealthMed.Application.Availabilities.CreateAvailability;
using HealthMed.Domain.AvailabilityAggregate;
using HealthMed.Domain.DoctorAggregate;
using NSubstitute;

namespace HealthMed.Unit.Tests.Availabilities;

[ExcludeFromCodeCoverage]
public sealed class CreateAvailabilityHandlerTests
{
    private readonly IAvailabilityRepository _availabilityRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateAvailabilityHandler _handler;

    public CreateAvailabilityHandlerTests()
    {
        _availabilityRepository = Substitute.For<IAvailabilityRepository>();
        _doctorRepository = Substitute.For<IDoctorRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new CreateAvailabilityHandler(_availabilityRepository, _unitOfWork, _doctorRepository);
    }

    [Fact]
    public async Task Handler_WithDateRange_ShouldAddMultiplesAvailability()
    {
        //Act
        var from = DateTimeOffset.Parse("2024-08-06T08:00:00");
        var to = DateTimeOffset.Parse("2024-08-06T12:00:00");
        var command = new CreateAvailabilityCommand(Guid.NewGuid(), IsAvailable: true, from, to, 30);

        _doctorRepository.Get(Arg.Any<Guid>()).Returns(new Fixture().Create<Doctor>());

        //Act
        await _handler.Handle(command, default);
    
        //Assert
        await _availabilityRepository.Received(8).Add(Arg.Any<Availability>());
    }
}