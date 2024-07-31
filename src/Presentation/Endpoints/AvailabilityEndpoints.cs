using HealthMed.Application.Availabilities.CreateAvailability;
using HealthMed.Application.Availabilities.GetAvailability;
using HealthMed.Presentation.Filters;
using MediatR;
using Nett.Core;

namespace HealthMed.Presentation.Endpoints;

internal static class AvailabilityEndpoints
{
    public static void MapAvailabilityEndpoints(this IEndpointRouteBuilder group)
    {
        group
            .MapGet("/", GetHandler)
            .RequireAuthorization("patient")
            .WithName("Get (availabilities)");

        group
            .MapPost("/", PostHandler)
            .Validate<CreateAvailabilityCommand>()
            .RequireAuthorization("doctor")
            .WithName("Create (availability)");

        group
            .MapPut("/", PutHandler)
            .Validate<UpdateAvailabilityCommand>()
            .RequireAuthorization("doctor")
            .WithName("Update (availability)");
    }

    private static async Task<Ok<IEnumerable<AvailabilityResponse>>> GetHandler(ISender sender, [AsParameters] GetAvailabilityQuery query) =>
        Ok(await sender.Send(query));

    private static async Task<Results<Ok<bool>, BadRequest<Error>>> PostHandler(ISender sender, CreateAvailabilityCommand command) =>
        (await sender.Send(command)).Match<Results<Ok<bool>, BadRequest<Error>>>((data) => Ok(data), (error) => BadRequest(error));

    private static async Task<Results<Ok<bool>, BadRequest<Error>>> PutHandler(ISender sender, UpdateAvailabilityCommand command) =>
        (await sender.Send(command)).Match<Results<Ok<bool>, BadRequest<Error>>>((data) => Ok(data), (error) => BadRequest(error));
}
