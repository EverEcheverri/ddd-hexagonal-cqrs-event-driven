using EventSagaDriven.Domain.SharedKernel.Exceptions;

namespace EventSagaDriven.Application.Account.Exceptions;

public class GenreDoesNotExistException : BusinessException
{
    public GenreDoesNotExistException(Guid genreId)
        : base($"genre Id: {genreId} does not exist")
    {
    }
}
