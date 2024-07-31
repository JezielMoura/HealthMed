using HealthMed.Application.Schedulings.CreateScheduling;
using MediatR;
using Nett.Core;

namespace HealthMed.Presentation.Endpoints;

internal static class SchedulingEndpoints
{
    public static void MapSchedulingEndpoints(this IEndpointRouteBuilder group)
    {
        group
            .MapPost("/", PostHandler)
            .RequireAuthorization("patient")
            .WithName("Create (scheduling)");
    }

    private static async Task<Results<Ok<Guid>, BadRequest<Error>>> PostHandler(ISender sender, CreateSchedulingCommand command) =>
        (await sender.Send(command)).Match<Results<Ok<Guid>, BadRequest<Error>>>((data) => Ok(data), (error) => BadRequest(error));
}
