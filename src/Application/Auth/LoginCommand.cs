using MediatR;

namespace HealthMed.Application.Auth;

public sealed record LoginCommand (string Email, string Password, bool IsDoctor = false) : IRequest<Result<string, Error>>;