namespace EventSagaDriven.Infrastructure.Services;

public interface IAuthTokenStore
{
    public Task<string> GetBearerTokenAsync(CancellationToken cancellationToken);
}
