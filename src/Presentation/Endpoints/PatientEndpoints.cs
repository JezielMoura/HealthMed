using HealthMed.Application.Patients.CreatePatient;
using HealthMed.Presentation.Filters;
using MediatR;
using Nett.Core;

namespace HealthMed.Presentation.Endpoints;

internal static class PatientEndpoints
{
    public static void MapPatientEndpoints(this IEndpointRouteBuilder group)
    {
        group
            .MapPost("/", PostHandler)
            .Validate<CreatePatientCommand>()
            .WithName("Create (patient)");
    }

    private static async Task<Results<Ok<Guid>, BadRequest<Error>>> PostHandler(ISender sender, CreatePatientCommand command) =>
        (await sender.Send(command)).Match<Results<Ok<Guid>, BadRequest<Error>>>((data) => Ok(data), (error) => BadRequest(error));
}
