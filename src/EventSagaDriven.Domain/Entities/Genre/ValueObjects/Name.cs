using EventSagaDriven.Domain.Entities.Genre.Exceptions;

namespace EventSagaDriven.Domain.Entities.Genre.ValueObjects;

public record Name
{
    private const short ValueMaxLength = 30;
    private readonly string _name;

    private Name(string value) => Value = value;
    public string Value { get; init; }

    public static Name Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new GenreNameNullOrEmptyException();
        }

        if (value.Length > ValueMaxLength)
        {
            throw new GenreNameMaxLengthException();
        }

        return new Name(value);
    }
}