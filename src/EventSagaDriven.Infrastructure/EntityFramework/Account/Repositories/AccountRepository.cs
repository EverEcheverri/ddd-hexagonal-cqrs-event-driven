using EventSagaDriven.Domain.Entities.Account.Repositories;
using EventSagaDriven.Domain.Entities.Account.ValueObjects;
using EventSagaDriven.Infrastructure.EntityFramework.Interfaces;
using EventSagaDriven.Infrastructure.Models.AppSettings;
using Microsoft.Extensions.Configuration;

namespace EventSagaDriven.Infrastructure.EntityFramework.Account.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly SqliteEventSagaDrivenContext _sqliteContext;
    private readonly IInfrastructureAccountRepository _repository;
    private const string SqliteAccountRepository = "SqliteAccountRepository";
    private const string CosmosAccountRepository = "CosmosAccountRepository";
    public AccountRepository(IConfiguration configuration,
        IEnumerable<IInfrastructureAccountRepository> repositories)
    {
        var databases = configuration.GetSection(Databases.DatabasesName);

        var useCosmos = databases.GetValue<bool>("Cosmos");
        var useSqlite = databases.GetValue<bool>("Sqlite");

        var repositoryName = useCosmos ? CosmosAccountRepository : SqliteAccountRepository;

        _repository = repositories.First(r => r.GetType().Name == repositoryName);
    }

    public async Task SaveAsync(Domain.Entities.Account.Account account, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _repository.SaveAsync(account, cancellationToken);
    }

    public async Task DeleteAsync(Email email, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(email, cancellationToken);
    }

    public async Task<Domain.Entities.Account.Account?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await _repository.GetByEmailAsync(email, cancellationToken);
    }
}
