using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using HealthMed.Application.Abstractions;
using HealthMed.Application.Auth;
using HealthMed.Domain.DoctorAggregate;
using HealthMed.Domain.PatientAggregate;
using NSubstitute;

namespace HealthMed.Unit.Tests.Auth;

[ExcludeFromCodeCoverage]
public sealed class LoginHandlerTests
{
    private IDoctorRepository _doctorRepository;
    private IPatientRepository _patientRepository;
    private ITokenProvider _tokenProvider;
    private LoginHandler _handler;

    public LoginHandlerTests()
    {
        _doctorRepository = Substitute.For<IDoctorRepository>();
        _patientRepository = Substitute.For<IPatientRepository>();
        _tokenProvider = Substitute.For<ITokenProvider>();
        _handler = new LoginHandler(_doctorRepository, _patientRepository, _tokenProvider);
    }

    [Fact]
    public async Task Handler_IsDoctorWithValidLogin_ShouldReturnToken()
    {
        //Arrange
        var passwordHash = "$2a$11$xo..hWz/pSvXYfu/AFdXdOOWrnukxDNoeg.UwfLQ03JpqfU78Y6PG";
        var email = "fake@email.com";
        var doctor = new Doctor(Guid.NewGuid(), "FakeName", email, "FakeCpf", "FakeCrm", passwordHash);
        var command = new LoginCommand(email, "12345678", IsDoctor: true);
        var fakeToken = "somejwttoken";

        _doctorRepository.Get(email).Returns(doctor);

        _tokenProvider.Create(doctor.Name, nameof(Doctor)).Returns(fakeToken);
    
        //Act
        var result = await _handler.Handle(command, default);
    
        //Assert
        result.Data.Should().BeEquivalentTo(fakeToken);
    }

    [Fact]
    public async Task Handler_IsDoctorWithInvalidLogin_ShouldReturnError()
    {
        //Arrange
        var email = "fake@email.com";
        var command = new LoginCommand(email, "invalidpassword", IsDoctor: true);
    
        //Act
        var result = await _handler.Handle(command, default);
    
        //Assert
        result.Error.Should().NotBeNull();
    }

    [Fact]
    public async Task Handler_IsPatientWithValidLogin_ShouldReturnToken()
    {
        //Arrange
        var passwordHash = "$2a$11$xo..hWz/pSvXYfu/AFdXdOOWrnukxDNoeg.UwfLQ03JpqfU78Y6PG";
        var email = "fake@email.com";
        var patient = new Patient(Guid.NewGuid(), "FakeName", email, "FakeCpf", passwordHash);
        var command = new LoginCommand(email, "12345678", IsDoctor: false);
        var fakeToken = "somejwttoken";

        _patientRepository.Get(email).Returns(patient);

        _tokenProvider.Create(patient.Name, nameof(Patient)).Returns(fakeToken);
    
        //Act
        var result = await _handler.Handle(command, default);
    
        //Assert
        result.Data.Should().BeEquivalentTo(fakeToken);
    }

    [Fact]
    public async Task Handler_IsPatientWithInvalidLogin_ShouldReturnError()
    {
        //Arrange
        var email = "fake@email.com";
        var command = new LoginCommand(email, "invalidpassword", IsDoctor: true);
    
        //Act
        var result = await _handler.Handle(command, default);
    
        //Assert
        result.Error.Should().NotBeNull();
    }
}