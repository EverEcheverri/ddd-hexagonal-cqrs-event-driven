namespace EventSagaDriven.Infrastructure.Models.AppSettings;

public class ApiBigBookOptions
{
    internal const string ApiBigBookOptionsName = "ApiBigBookOptions";
    public string ApiKey { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string OauthBaseUrl { get; set; }
    public string OauthClientKey { get; set; }
    public required ApiBigBookServiceOptions Service { get; set; }
}

public record ApiBigBookServiceOptions(string Scheme, string Host, string Path);
