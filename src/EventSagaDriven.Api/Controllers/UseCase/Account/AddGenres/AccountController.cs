using EventSagaDriven.Api.Middleware;
using EventSagaDriven.Application.Account.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EventSagaDriven.Api.Controllers.UseCase.Account.AddGenres;

[Route("api/Genres")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAddAccountGenres _addAccountGenres;
    public AccountController(IAddAccountGenres addAccountGenres)
    {
        _addAccountGenres = addAccountGenres;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CustomErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CustomErrorResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(CustomErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddAccountGenresAsync([FromBody][Required] RequestAddGenres requestAddGenres,
    CancellationToken cancellationToken)
    {
        var command = requestAddGenres.ToAddGenresCommand();
        await _addAccountGenres.ExecuteAsync(command, cancellationToken);

        return StatusCode((int)HttpStatusCode.Created);
    }
}
