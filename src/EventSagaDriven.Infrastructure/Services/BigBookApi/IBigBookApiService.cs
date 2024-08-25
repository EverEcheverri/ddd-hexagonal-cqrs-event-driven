using EventSagaDriven.Domain.ThirdPartyBookService;
using Refit;

namespace EventSagaDriven.Infrastructure.Services.BigBookApi;

//[Headers("Autorization: Bearer")]
public interface IBigBookApiService
{
    [Get("/search-books")]
    public Task<ApiResponse<ThirdPartyBookResult>> SearchBooksByGenresAsync(
      [Query] string genres, CancellationToken cancellationToken = default);
}
