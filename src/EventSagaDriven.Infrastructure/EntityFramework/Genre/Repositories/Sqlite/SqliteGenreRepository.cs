using EventSagaDriven.Domain.Entities.Genre.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventSagaDriven.Infrastructure.EntityFramework.Genre.Repositories.Sqlite;

public class SqliteGenreRepository : IGenreRepository
{
    private readonly SqliteEventSagaDrivenContext _context;
    public SqliteGenreRepository(SqliteEventSagaDrivenContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Genre.Genre> GetByIdAsync(Guid genreId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _context.Genres
            .FirstOrDefaultAsync(g => g.Id == genreId, cancellationToken);
    }
}
