using EventSagaDriven.Domain.SharedKernel.Exceptions;

namespace EventSagaDriven.Domain.Entities.Account.Exceptions;

public class MobileNoValidNumberException : BusinessException
{
    public MobileNoValidNumberException() : base("Mobile number is not valid")
    {
    }
}
