using EventSagaDriven.Api.Controllers.UseCase.Account.GetByEmail;
using EventSagaDriven.Api.Middleware;
using EventSagaDriven.Application.ThirdPartyBook.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EventSagaDriven.Api.Controllers.UseCase.ThirdPartyBook.GetThirdPartyBook
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThirdPartyBooksController : ControllerBase
    {
        private readonly IGetThirdPartyBooks _useCase;
        public ThirdPartyBooksController(IGetThirdPartyBooks useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseAccountByEmail))]
        [ProducesResponseType(typeof(CustomErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAccountByIdAsync([FromQuery][Required] string genres,
            CancellationToken cancellationToken)
        {
            var books = await _useCase.ExecuteAsync(genres, cancellationToken);
            if (!books.Any())
            {
                return NoContent();
            }
            return StatusCode((int)HttpStatusCode.OK, books);
        }
    }
}
