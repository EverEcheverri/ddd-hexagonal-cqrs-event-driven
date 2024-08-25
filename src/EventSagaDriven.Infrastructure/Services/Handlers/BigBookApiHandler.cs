using System.Net.Http.Headers;

namespace EventSagaDriven.Infrastructure.Services.Handlers;

public class BigBookApiHandler : DelegatingHandler
{
    private readonly IAuthTokenStore _authTokenStore;

    public BigBookApiHandler(IAuthTokenStore authTokenStore)
    {
        _authTokenStore = authTokenStore;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        var token = await _authTokenStore.GetBearerTokenAsync(cancellationToken);

        //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}
