using EventSagaDriven.Domain.ExternalBooksService;
using EventSagaDriven.Domain.ThirdPartyBookService;
using EventSagaDriven.Infrastructure.Services.BigBookApi;

namespace EventSagaDriven.Infrastructure.ExternalBooksService;

internal class ExternalBooksService : IThirdPartyBookService
{
    private readonly IBigBookApiService _service;

    public ExternalBooksService(IBigBookApiService service)
    {
        _service = service;
    }

    public async Task<ThirdPartyBookResult?> GetExternalBooksAsync(string genres, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.SearchBooksByGenresAsync(genres, cancellationToken);
            //var json = result?.Content;
            //var thirdPartyBookResult
            //    = JsonSerializer.Deserialize<ThirdPartyBookResult>(json);
            //return thirdPartyBookResult;
            return result?.Content;
        }
        catch (Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            throw new Exception(ex.Message);
        }
    }
}
