using EventSagaDriven.Domain.Entities.Account.Exceptions;
using System.Text.RegularExpressions;

namespace EventSagaDriven.Domain.Entities.Account.ValueObjects;

public record Email
{
    private const short EmailMaxLength = 254;
    private Email(string value) => Value = value;

    public string Value { get; init; }

    public static Email Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmailNullOrEmptyException();
        }        
        
        if (value.Length > EmailMaxLength)
        {
            throw new EmailMaxLengthException();
        }

        if (!IsValidEmail(value))
        {
            throw new EmailInvalidPatternException();
        }

        return new Email(value);
    }

    private static bool IsValidEmail(string value)
    {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(value, pattern);
    }
}