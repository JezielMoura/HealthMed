namespace HealthMed.Application.Abstractions;

public interface IUnitOfWork
{
    Task<Result<bool, Error>> Commit(CancellationToken cancellationToken = default);
    Task<Result<Guid, Error>> Commit(Guid id, CancellationToken cancellationToken = default);
}