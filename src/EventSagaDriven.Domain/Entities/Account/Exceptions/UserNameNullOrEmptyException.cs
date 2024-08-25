using EventSagaDriven.Domain.SharedKernel.Exceptions;

namespace EventSagaDriven.Domain.Entities.Account.Exceptions;

public class UserNameNullOrEmptyException : BusinessException
{
    public UserNameNullOrEmptyException() : base("User name is null or empty")
    {
    }
}
