using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using HealthMed.Application.Abstractions;
using HealthMed.Application.Patients.CreatePatient;
using HealthMed.Domain.PatientAggregate;
using NSubstitute;

namespace HealthMed.Unit.Tests.Patients;

[ExcludeFromCodeCoverage]
public sealed class CreatePatientHandlerTests
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreatePatientHandler _handler;

    public CreatePatientHandlerTests()
    {
        _patientRepository = Substitute.For<IPatientRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new CreatePatientHandler(_patientRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldCallRepositoryAdd()
    {
        //Arrange
        var command = new Fixture().Create<CreatePatientCommand>();
    
        //Act
        await _handler.Handle(command, default);
    
        //Assert
        await _patientRepository.Received(1).Add(Arg.Any<Patient>());
    }
}