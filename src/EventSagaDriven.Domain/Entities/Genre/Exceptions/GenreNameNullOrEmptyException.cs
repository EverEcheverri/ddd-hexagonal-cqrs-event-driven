using EventSagaDriven.Domain.SharedKernel.Exceptions;

namespace EventSagaDriven.Domain.Entities.Genre.Exceptions;

public class GenreNameNullOrEmptyException : BusinessException
{
    public GenreNameNullOrEmptyException() : base("Genre name is null or empty")
    {
    }
}