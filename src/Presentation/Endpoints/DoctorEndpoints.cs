using HealthMed.Application.Doctors.CreateDoctor;
using HealthMed.Presentation.Filters;
using MediatR;
using Nett.Core;

namespace HealthMed.Presentation.Endpoints;

internal static class DoctorEndpoints
{
    public static void MapDoctorEndpoints(this IEndpointRouteBuilder group)
    {
        group
            .MapPost("/", PostHandler)
            .Validate<CreateDoctorCommand>()
            .WithName("Create (doctor)");
    }

    private static async Task<Results<Ok<Guid>, BadRequest<Error>>> PostHandler(ISender sender, CreateDoctorCommand command) =>
        (await sender.Send(command)).Match<Results<Ok<Guid>, BadRequest<Error>>>((data) => Ok(data), (error) => BadRequest(error));
}
