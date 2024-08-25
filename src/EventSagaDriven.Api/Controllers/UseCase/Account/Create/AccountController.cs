using EventSagaDriven.Api.Middleware;
using EventSagaDriven.Application.Account.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EventSagaDriven.Api.Controllers.UseCase.Account.Create;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ICreateAccount _createAccount;
    public AccountController(ICreateAccount createAccount)
    {
        _createAccount = createAccount;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CustomErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CustomErrorResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(CustomErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAccountAsync([FromBody][Required] RequestCreateAccount requestCreateAccount,
    CancellationToken cancellationToken)
    {
        var command = requestCreateAccount.ToCreateAccountCommand();
        await _createAccount.ExecuteAsync(command, cancellationToken);

        return StatusCode((int)HttpStatusCode.Created, requestCreateAccount.Email);
    }
}
