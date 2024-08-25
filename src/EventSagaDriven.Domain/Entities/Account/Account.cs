using EventSagaDriven.Domain.Entities.Account.Enums;
using EventSagaDriven.Domain.Entities.Account.Events;
using EventSagaDriven.Domain.Entities.Account.Exceptions;
using EventSagaDriven.Domain.Entities.Account.ValueObjects;
using EventSagaDriven.Domain.SharedKernel;

namespace EventSagaDriven.Domain.Entities.Account;

public sealed class Account : Entity
{
    public static readonly string AccountGenresName = nameof(AccountGenres);
    private readonly List<AccountGenre> _accountGenres = new List<AccountGenre>();
    private Account()
    {
    }

    private Account(Guid id, Email email,
        UserName userName,
        Mobile mobile,
        AccountType accountType,
        Guid cityId)
    {
        Id = id;
        Email = email;
        UserName = userName;
        Mobile = mobile;
        AccountType = accountType;
        CityId = cityId;
    }

    public Guid Id { get; private set; }
    public Email Email { get; private set; }
    public UserName UserName { get; private set; }
    public Mobile Mobile { get; private set; }
    public AccountType AccountType { get; private set; }
    public Guid CityId { get; private set; }

    public IReadOnlyCollection<AccountGenre> AccountGenres => _accountGenres;
    public IReadOnlyCollection<Genre.Genre> Genres()
    {
        return AccountGenres.Select(e => e.Genre).ToList();
    }


    public static Account Build(Guid id, Email email, UserName userName, Mobile mobile, AccountType accountType, Guid cityId)
    {
        if (cityId.ToString().Equals(Guid.Empty.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            throw new NoValidCityIdException();
        }

        var account = new Account(id, email, userName, mobile, accountType, cityId);
        account.AccountCreatedEvent();
        return account;
    }

    public void AddGenres(List<Genre.Genre> genres)
    {
        if (genres == null || genres.Count == 0)
        {
            return;
        }

        var currentGenresIds = _accountGenres.Select(ag => ag.Genre.Id).ToList();
        var newGenresIds = genres.Select(g => g.Id).ToList();

        if (!newGenresIds.All(newGenreId => currentGenresIds.Any(currentGenresId => currentGenresId == newGenreId)))
        {
            newGenresIds.Except(currentGenresIds).ToList().ForEach(genreId =>
            {
                var newGenre = genres.First(c => c.Id == genreId);
                _accountGenres.Add(AccountGenre.Build(this, newGenre));
            });
        }

    }
}