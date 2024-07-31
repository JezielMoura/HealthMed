using HealthMed.Application.Auth;
using MediatR;
using Nett.Core;

namespace HealthMed.Presentation.Endpoints;

internal static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder group)
    {
        group
            .MapPost("/", PostHandler)
            .WithName("Auth");
    }

    private static async Task<Results<Ok<string>, BadRequest<Error>>> PostHandler(ISender sender, LoginCommand command) =>
        (await sender.Send(command)).Match<Results<Ok<string>, BadRequest<Error>>>((data) => Ok(data), (error) => BadRequest(error));
}
