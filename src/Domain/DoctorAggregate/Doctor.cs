namespace HealthMed.Domain.DoctorAggregate;

public sealed class Doctor
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string CPF { get; private set; }
    public string CRM { get; private set; }
    public string Password { get; private set; }

    public Doctor(Guid id, string name, string email, string cpf, string crm, string password)
    {
        Id = id;
        Name = name;
        Email = email;
        CPF = cpf;
        CRM = crm;
        Password = password;
    }
}
