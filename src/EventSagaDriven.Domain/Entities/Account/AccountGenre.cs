namespace EventSagaDriven.Domain.Entities.Account;

public class AccountGenre
{  
    public Account Account { get; private set; }
    public Genre.Genre Genre { get; private set; }

    public static AccountGenre Build(Account account, Genre.Genre genre)
    {
        return new AccountGenre { Account = account, Genre = genre };
    }
}