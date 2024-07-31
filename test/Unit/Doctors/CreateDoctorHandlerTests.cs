using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using HealthMed.Application.Abstractions;
using HealthMed.Application.Doctors.CreateDoctor;
using HealthMed.Domain.DoctorAggregate;
using NSubstitute;

namespace HealthMed.Unit.Tests.Doctors;

[ExcludeFromCodeCoverage]
public sealed class CreateDoctorHandlerTests
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateDoctorHandler _handler;

    public CreateDoctorHandlerTests()
    {
        _doctorRepository = Substitute.For<IDoctorRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new CreateDoctorHandler(_doctorRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldCallRepositoryAdd()
    {
        //Arrange
        var command = new Fixture().Create<CreateDoctorCommand>();
    
        //Act
        await _handler.Handle(command, default);
    
        //Assert
        await _doctorRepository.Received(1).Add(Arg.Any<Doctor>());
    }
}