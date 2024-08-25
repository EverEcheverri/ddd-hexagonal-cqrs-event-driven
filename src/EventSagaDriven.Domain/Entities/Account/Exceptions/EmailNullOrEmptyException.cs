using EventSagaDriven.Domain.SharedKernel.Exceptions;

namespace EventSagaDriven.Domain.Entities.Account.Exceptions;
public class EmailNullOrEmptyException : BusinessException
{
    public EmailNullOrEmptyException() : base("Email is null or empty")
    {
    }
}
