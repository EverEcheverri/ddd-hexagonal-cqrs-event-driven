using EventSagaDriven.Infrastructure.Models;
using EventSagaDriven.Infrastructure.Services.BigBookApi;

namespace EventSagaDriven.Infrastructure.Services;

public class AuthTokenStore : IAuthTokenStore
{
    private AuthTokenResponse? _authToken;
    private readonly IBigBookApiAuthService _bigBookApiAuthService;

    public AuthTokenStore(IBigBookApiAuthService bigBookApiAuthService)
    {
        _bigBookApiAuthService = bigBookApiAuthService;
    }

    public async Task<string> GetBearerTokenAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (_authToken is { IsExpired: false })
            {
                return _authToken.AccessToken;
            }

            var res = await _bigBookApiAuthService.GenerateAuthTokenAsync(
                new Dictionary<string, string>
                {
                    {"grant_type", "client_credentials" }
                }
            );

            _authToken = res.Content!;
            return _authToken.AccessToken;
        }
        catch (Exception ex)
        {
            return "No auth token needed in this flow";
        }
    }
}
