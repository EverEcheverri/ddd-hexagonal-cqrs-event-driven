using EventSagaDriven.Application.ThirdPartyBook.Interfaces;
using EventSagaDriven.Domain.ExternalBooksService;
using EventSagaDriven.Domain.ThirdPartyBookService;

namespace EventSagaDriven.Application.ThirdPartyBook.UseCases;

public class GetThirdPartyBooksUseCase : IGetThirdPartyBooks
{
    private readonly IThirdPartyBookService _externalBooksService;

    public GetThirdPartyBooksUseCase(IThirdPartyBookService externalBooksService)
    {
        _externalBooksService = externalBooksService;
    }
    public async Task<List<Book>> ExecuteAsync(string genres, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = await _externalBooksService.GetExternalBooksAsync(genres, cancellationToken);

        var books = result?.Books.SelectMany(row => row).ToList();

        return books ?? [];
    }
}
