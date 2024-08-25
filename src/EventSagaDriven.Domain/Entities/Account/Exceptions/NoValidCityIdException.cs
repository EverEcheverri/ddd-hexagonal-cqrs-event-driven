using EventSagaDriven.Domain.SharedKernel.Exceptions;

namespace EventSagaDriven.Domain.Entities.Account.Exceptions;

public class NoValidCityIdException : BusinessException
{
    public NoValidCityIdException() : base("City id is null or empty")
    {
    }
}
