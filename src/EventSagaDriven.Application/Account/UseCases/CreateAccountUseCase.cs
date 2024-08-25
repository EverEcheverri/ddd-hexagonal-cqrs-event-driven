using EventSagaDriven.Application.Account.Exceptions;
using EventSagaDriven.Application.Account.Interfaces;
using EventSagaDriven.Domain.Entities.Account.Commands;
using EventSagaDriven.Domain.Entities.Account.Repositories;

namespace EventSagaDriven.Application.Account.UseCases;

public class CreateAccountUseCase : ICreateAccount
{
    private readonly IAccountRepository _accountRepository;

    public CreateAccountUseCase(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task ExecuteAsync(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var accountExists = await _accountRepository.GetByEmailAsync(command.Email, cancellationToken);
        if (accountExists is not null)
        {
            throw new AccountEmailAlreadyExistException(command.Email.Value);
        }

        var account = Domain.Entities.Account.Account.Build(
            command.Id,
            command.Email,
            command.UserName,
            command.Mobile,
            command.AccountType,
            command.CityId);

        await _accountRepository.SaveAsync(account, cancellationToken);
    }
}
