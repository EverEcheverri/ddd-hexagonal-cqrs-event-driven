namespace EventSagaDriven.Api.Middleware;

public class LoggingEvents
{
    public static readonly int Unknown = 000;

    public static readonly int EmailInvalidPattern = 100;
    public static readonly int EmailMaxLength = 101;
    public static readonly int EmailNullOrEmpty = 102;
    public static readonly int MobileNullOrEmpty = 103;
    public static readonly int MobileNumberInvalidCharacter = 104;
    public static readonly int MobileNumberMaxLength = 105;
    public static readonly int MobileNumberMinLength = 106;
    public static readonly int NoValidCityId = 107;
    public static readonly int UserNameMaxLength = 108;
    public static readonly int UserNameNullOrEmpty = 109;

    public static readonly int AccountEmailAlreadyExist = 150;




    public static readonly int GeneralValidationError = 199;
}