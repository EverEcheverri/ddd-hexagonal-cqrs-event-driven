using EventSagaDriven.Domain.Entities.Account.ValueObjects;

namespace EventSagaDriven.Infrastructure.EntityFramework.Interfaces;

public  interface IInfrastructureAccountRepository
{
    Task SaveAsync(Domain.Entities.Account.Account account, CancellationToken cancellationToken);
    Task<Domain.Entities.Account.Account?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
    Task DeleteAsync(Email email, CancellationToken cancellationToken);
}
