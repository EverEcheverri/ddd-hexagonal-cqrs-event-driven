using EventSagaDriven.Domain.Entities.Account;
using EventSagaDriven.Domain.Entities.Account.ValueObjects;
using EventSagaDriven.Domain.Entities.Genre;
using EventSagaDriven.Domain.SharedKernel.Events;
using EventSagaDriven.Infrastructure.EntityFramework.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EventSagaDriven.Infrastructure.EntityFramework.Account.Repositories.Cosmos;

public class CosmosAccountRepository : IInfrastructureAccountRepository
{
    private readonly CosmosEventSagaDrivenContext _context;
    public CosmosAccountRepository(CosmosEventSagaDrivenContext context)
    {
        _context = context;
    }
    public async Task<Domain.Entities.Account.Account?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await _context.Accounts
           .FirstOrDefaultAsync(a => a.Email == email, cancellationToken);
    }

    public async Task SaveAsync(Domain.Entities.Account.Account account, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var existingAccount = await _context.Accounts
            .SingleOrDefaultAsync(f => f.Email == account.Email, cancellationToken);

        if (existingAccount == null)
        {
            await _context.Accounts.AddAsync(account, cancellationToken);
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
