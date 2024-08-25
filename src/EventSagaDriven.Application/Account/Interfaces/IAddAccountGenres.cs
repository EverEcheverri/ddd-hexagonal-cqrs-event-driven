using EventSagaDriven.Domain.Entities.Account.Commands;

namespace EventSagaDriven.Application.Account.Interfaces;

public interface IAddAccountGenres
{
    Task ExecuteAsync(AddGenresCommand addGenresCommand, CancellationToken cancellationToken);
}
