using System.Text.Json.Serialization;

namespace EventSagaDriven.Infrastructure.Models;

public class AuthTokenResponse
{
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    public DateTime ObtainedAt { get; init; } = DateTime.UtcNow;

    public bool IsExpired => DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(ExpiresIn)) > ObtainedAt;

    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
}
