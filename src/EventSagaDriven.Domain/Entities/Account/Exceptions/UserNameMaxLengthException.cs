using EventSagaDriven.Domain.SharedKernel.Exceptions;

namespace EventSagaDriven.Domain.Entities.Account.Exceptions;

public class UserNameMaxLengthException : BusinessException
{
    public UserNameMaxLengthException() : base("User name max length exceeded")
    {
    }
}
