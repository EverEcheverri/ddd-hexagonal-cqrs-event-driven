using EventSagaDriven.Application.Account.Exceptions;
using EventSagaDriven.Application.Account.Interfaces;
using EventSagaDriven.Domain.Entities.Account.Commands;
using EventSagaDriven.Domain.Entities.Account.Repositories;
using EventSagaDriven.Domain.Entities.Genre;
using EventSagaDriven.Domain.Entities.Genre.Repositories;

namespace EventSagaDriven.Application.Account.UseCases;

public class AddAccountGenresUseCase : IAddAccountGenres
{
    private readonly IAccountRepository _accountRepository;
    private readonly IGenreRepository _genreRepository;

    public AddAccountGenresUseCase(IAccountRepository accountRepository, IGenreRepository genreRepository)
    {
        _accountRepository = accountRepository;
        _genreRepository = genreRepository;
    }

    public async Task ExecuteAsync(AddGenresCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var account = await _accountRepository.GetByEmailAsync(command.Email, cancellationToken);
        if (account is null)
        {
            throw new AccountDoesNotExistException(command.Email.Value);
        }

        var genres = new List<Genre>();

        foreach (var genreId in command.GenreIds)
        {
            var genre = await _genreRepository.GetByIdAsync(genreId, cancellationToken);

            if (genre is null)
            {
                throw new GenreDoesNotExistException(genreId);
            }
            genres.Add(genre);
        }

        account.AddGenres(genres);
        await _accountRepository.SaveAsync(account, cancellationToken);
    }
}
