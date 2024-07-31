namespace HealthMed.Application.Abstractions;

public interface ITokenProvider
{
    Task<string> Create(string name, string role);
}