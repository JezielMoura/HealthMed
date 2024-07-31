using FluentValidation;
using HealthMed.Application.Patients.CreatePatient;
using HealthMed.Infrastructure.Extensions;
using HealthMed.Presentation.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureSwagger();
builder.Services.ConfigureRepositories();
builder.Services.ConfigureEntityFramework(builder.Configuration);
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<CreatePatientCommand>();
builder.Services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining<CreatePatientCommand>());
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("api/auth").MapAuthEndpoints();
app.MapGroup("api/availabilities").MapAvailabilityEndpoints();
app.MapGroup("api/doctors").MapDoctorEndpoints();
app.MapGroup("api/patients").MapPatientEndpoints();
app.MapGroup("api/schedulings").MapSchedulingEndpoints();

app.Run();
