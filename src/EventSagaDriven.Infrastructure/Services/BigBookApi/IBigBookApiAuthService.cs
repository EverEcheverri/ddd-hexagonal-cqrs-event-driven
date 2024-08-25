using EventSagaDriven.Infrastructure.Models;
using Refit;

namespace EventSagaDriven.Infrastructure.Services.BigBookApi;

public interface IBigBookApiAuthService
{
    [Post("/v1/token")]
    public Task<ApiResponse<AuthTokenResponse>> GenerateAuthTokenAsync(
        [Body(BodySerializationMethod.UrlEncoded)]
        Dictionary<string, string> data,
        CancellationToken cancellationToken = default);

}
