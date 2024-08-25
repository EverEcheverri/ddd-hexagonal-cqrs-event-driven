using EventSagaDriven.Domain.Entities.Account.ValueObjects;

namespace EventSagaDriven.Application.Account.Interfaces;

public interface IGetAccountByEmail
{
    Task<Domain.Entities.Account.Account?> ExecuteAsync(Email email, CancellationToken cancellationToken);
}
