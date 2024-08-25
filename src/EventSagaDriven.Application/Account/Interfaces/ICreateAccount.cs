using EventSagaDriven.Domain.Entities.Account.Commands;

namespace EventSagaDriven.Application.Account.Interfaces
{
    public interface ICreateAccount
    {
        Task ExecuteAsync(CreateAccountCommand createAccountCommand, CancellationToken cancellationToken);
    }
}
