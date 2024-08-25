using EventSagaDriven.Domain.SharedKernel.Exceptions;

namespace EventSagaDriven.Domain.Entities.Account.Exceptions;

public class MobileNullOrEmptyException : BusinessException
{
    public MobileNullOrEmptyException() : base("Mobile is null or empty")
    {
    }
}
