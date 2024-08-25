
using EventSagaDriven.Domain.Entities.Account;
using EventSagaDriven.Domain.Entities.Genre.ValueObjects;
using EventSagaDriven.Domain.SharedKernel;

namespace EventSagaDriven.Domain.Entities.Genre;

public sealed class Genre : Entity
{
    public static readonly string AccountGenresName = nameof(AccountGenres);
    private readonly List<AccountGenre> _accountGenres = new List<AccountGenre>();
    private Genre()
    {
    }

    private Genre(Guid id, Name name, Description description)
    {
        Id = id;
        Name = name;
        Description = description;
    }


    public Guid Id { get; private set; }
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public IReadOnlyCollection<AccountGenre> AccountGenres => _accountGenres;

    public static Genre Build(Guid id, Name name, Description description)
    {
        return new Genre(id, name, description);
    }
}
