using System.Diagnostics.CodeAnalysis;

namespace HealthMed.Domain.PatientAggregate;

public sealed class Patient
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string CPF { get; private set; }
    public string Password { get; private set; }

    public Patient(Guid id, string name, string email, string cpf, string password)
    {
        Id = id;
        Name = name;
        Email = email;
        CPF = cpf;
        Password = password;
    }

    #nullable disable
    [ExcludeFromCodeCoverage]
    private Patient() {}
}