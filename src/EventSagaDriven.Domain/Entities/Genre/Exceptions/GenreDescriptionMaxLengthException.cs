using EventSagaDriven.Domain.SharedKernel.Exceptions;

namespace EventSagaDriven.Domain.Entities.Genre.Exceptions;

public class GenreDescriptionMaxLengthException : BusinessException
{
    public GenreDescriptionMaxLengthException() : base("Genre description max length exceeded")
    {
    }
}