using EventSagaDriven.Domain.SharedKernel.Exceptions;

namespace EventSagaDriven.Application.Account.Exceptions;

internal class AccountDoesNotExistException : BusinessException
{
    public AccountDoesNotExistException(string email)
        : base($"account with email {email} does not exist")
    {
    }
}
