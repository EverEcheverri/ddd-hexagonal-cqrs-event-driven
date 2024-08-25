using EventSagaDriven.Domain.SharedKernel.Exceptions;

namespace EventSagaDriven.Application.Account.Exceptions;

public class AccountEmailAlreadyExistException : BusinessException
{
    public AccountEmailAlreadyExistException(string email) 
        : base($"account with email {email} already exist")
    {
    }
}
