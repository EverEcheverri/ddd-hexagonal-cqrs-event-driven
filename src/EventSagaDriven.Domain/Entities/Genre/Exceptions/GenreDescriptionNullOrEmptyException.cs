using EventSagaDriven.Domain.SharedKernel.Exceptions;

namespace EventSagaDriven.Domain.Entities.Genre.Exceptions;

public class GenreDescriptionNullOrEmptyException : BusinessException
{
    public GenreDescriptionNullOrEmptyException() : base("Genre description is null or empty")
    {
    }
}