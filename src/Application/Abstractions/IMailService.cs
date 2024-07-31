namespace HealthMed.Application.Abstractions;

public interface IMailService
{
    Task Send(string DoctorEmail, string DoctorName, string PatientName, DateTimeOffset date);
}