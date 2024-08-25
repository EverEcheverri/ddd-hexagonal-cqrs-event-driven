using EventSagaDriven.Domain.SharedKernel.Exceptions;

namespace EventSagaDriven.Domain.Entities.Account.Exceptions;

public class EmailInvalidPatternException : BusinessException
{
    public EmailInvalidPatternException() : base("Email invalid pattern")
    {
    }
}
