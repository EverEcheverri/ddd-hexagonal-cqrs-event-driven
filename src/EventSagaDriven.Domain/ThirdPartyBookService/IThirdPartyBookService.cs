using EventSagaDriven.Domain.ThirdPartyBookService;

namespace EventSagaDriven.Domain.ExternalBooksService;

public interface IThirdPartyBookService
{
    Task<ThirdPartyBookResult?> GetExternalBooksAsync(string genres, CancellationToken cancellationToken);
}
