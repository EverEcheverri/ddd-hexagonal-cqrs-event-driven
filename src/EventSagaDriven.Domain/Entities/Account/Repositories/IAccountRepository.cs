using EventSagaDriven.Domain.Entities.Account.ValueObjects;

namespace EventSagaDriven.Domain.Entities.Account.Repositories;

public interface IAccountRepository
{
    Task SaveAsync(Account account, CancellationToken cancellationToken);
    Task<Account?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
    Task DeleteAsync(Email email, CancellationToken cancellationToken);
}
