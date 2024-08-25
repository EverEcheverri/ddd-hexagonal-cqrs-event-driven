using EventSagaDriven.Domain.Entities.Account.Exceptions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace EventSagaDriven.Domain.Entities.Account.ValueObjects;

public record Mobile
{
    private Mobile(string value) => Value = value;

    public string Value { get; init; }

    public static Mobile Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new MobileNullOrEmptyException();
        }

        if (!IsValidPhoneNumber(value))
        {
            throw new MobileNoValidNumberException();
        }
        return new Mobile(value);
    }
    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        string pattern = @"^\d{10}$";
        return Regex.IsMatch(phoneNumber, pattern);
    }
}