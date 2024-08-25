using EventSagaDriven.Domain.ExternalBooksService;
using EventSagaDriven.Domain.ThirdPartyBookService;

namespace EventSagaDriven.Application.ThirdPartyBook.Interfaces;

public interface IGetThirdPartyBooks
{
    Task<List<Book>> ExecuteAsync(string genres, CancellationToken cancellationToken);
}
