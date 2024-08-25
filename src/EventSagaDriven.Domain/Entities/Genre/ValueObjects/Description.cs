using EventSagaDriven.Domain.Entities.Genre.Exceptions;

namespace EventSagaDriven.Domain.Entities.Genre.ValueObjects;

public record Description
{
    private const short ValueMaxLength = 100;
    private readonly string _description;

    private Description(string value) => Value = value;

    public string Value { get; init; }

    public static Description Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new GenreDescriptionNullOrEmptyException();
        }

        if (value.Length > ValueMaxLength)
        {
            throw new GenreDescriptionMaxLengthException();
        }

        return new Description(value);
    }
}

