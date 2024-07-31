using HealthMed.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace HealthMed.Infrastructure.Services;

internal sealed class MailService : IMailService
{
    private readonly IConfiguration _configuration;

    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task Send(string doctorEmail, string doctorName, string patientName, DateTimeOffset date)
    {
        var host = _configuration["MailSettings:Host"] ?? throw new ArgumentException("Mail host cannot be empty");
        var port = _configuration["MailSettings:Port"] ?? throw new ArgumentException("Mail port cannot be empty");
        var user = _configuration["MailSettings:User"] ?? throw new ArgumentException("Mail user cannot be empty");
        var password = _configuration["MailSettings:Password"] ?? throw new ArgumentException("Mail password cannot be empty");

        var smtpClient = new SmtpClient(host, int.Parse(port))
        {
            Credentials = new NetworkCredential(user, password)
        };

        var title = "Health&Med - Nova consulta agendada";
        var body = new StringBuilder();

        body.AppendLine($"Olá, Dr. {doctorName}");
        body.AppendLine("Você tem uma nova consulta marcada!");
        body.AppendLine($"Paciente: {patientName}");
        body.AppendLine($"Data e horário: {date:dd/MM/yyyy} às {date:HH:mm}.");

        var mailMessage = new MailMessage(user, doctorEmail, title, body.ToString());

        await smtpClient.SendMailAsync(mailMessage);
    }
}