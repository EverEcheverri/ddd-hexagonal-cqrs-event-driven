using EventSagaDriven.Domain.SharedKernel.Exceptions;

namespace EventSagaDriven.Domain.Entities.Account.Exceptions;

public class EmailMaxLengthException : BusinessException
{
    public EmailMaxLengthException() : base("Email max length exceeded 254 characters")
    {
    }
}

