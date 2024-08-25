namespace EventSagaDriven.Infrastructure.EntityFramework.Account.Repositories.Sqlite;

using EventSagaDriven.Domain.Entities.Account.ValueObjects;
using EventSagaDriven.Infrastructure.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;

public class SqliteAccountRepository : IInfrastructureAccountRepository
{
    private readonly SqliteEventSagaDrivenContext _context;
    public SqliteAccountRepository(SqliteEventSagaDrivenContext context)
    {
        _context = context;
    }
    public async Task<Domain.Entities.Account.Account?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _context.Accounts
            .Include(a => a.AccountGenres)
            .ThenInclude(a => a.Genre)
            .FirstOrDefaultAsync(a => a.Email == email, cancellationToken);
    }

    public async Task SaveAsync(Domain.Entities.Account.Account account, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var existingAccount = _context.Accounts.Find(account.Id);

        if (existingAccount == null)
        {
            await _context.Accounts.AddAsync(account, cancellationToken);
        }
        else
        {
            _context.Entry(account).State = EntityState.Modified;
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Domain.Entities.Account.Account account, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        try
        {
            _context.Entry(account).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task DeleteAsync(Email email, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var account = await _context.Accounts
                            .FirstOrDefaultAsync(a => a.Email == email, cancellationToken);

        _context.Accounts.Remove(account);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
