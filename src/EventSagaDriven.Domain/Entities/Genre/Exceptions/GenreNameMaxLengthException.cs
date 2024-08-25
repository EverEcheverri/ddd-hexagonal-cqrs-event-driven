using EventSagaDriven.Domain.SharedKernel.Exceptions;

namespace EventSagaDriven.Domain.Entities.Genre.Exceptions;

public class GenreNameMaxLengthException : BusinessException
{
    public GenreNameMaxLengthException() : base("Genre name max length exceeded")
    {
    }
}