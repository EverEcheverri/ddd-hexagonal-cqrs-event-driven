namespace EventSagaDriven.Domain.Entities.Genre.Repositories;

public interface IGenreRepository
{
    Task<Genre> GetByIdAsync(Guid genreId, CancellationToken cancellationToken);
}
