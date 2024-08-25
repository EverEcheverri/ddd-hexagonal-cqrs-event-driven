using EventSagaDriven.Domain.Entities.Account.Exceptions;

namespace EventSagaDriven.Domain.Entities.Account.ValueObjects;
public record UserName
{
    private UserName(string value) => Value = value;

    public string Value { get; init; }

    public static UserName Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new UserNameNullOrEmptyException();
        }
        return new UserName(value);
    }
}