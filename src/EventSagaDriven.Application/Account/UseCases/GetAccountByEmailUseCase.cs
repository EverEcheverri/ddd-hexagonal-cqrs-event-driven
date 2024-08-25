using EventSagaDriven.Application.Account.Interfaces;
using EventSagaDriven.Domain.Entities.Account.Repositories;
using EventSagaDriven.Domain.Entities.Account.ValueObjects;

namespace EventSagaDriven.Application.Account.UseCases;

public class GetAccountByEmailUseCase : IGetAccountByEmail
{
    private readonly IAccountRepository _accountRepository;
    public GetAccountByEmailUseCase(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Domain.Entities.Account.Account?> ExecuteAsync(Email email, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await _accountRepository.GetByEmailAsync(email, cancellationToken);
    }
}
