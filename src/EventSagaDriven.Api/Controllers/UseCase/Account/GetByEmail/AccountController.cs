using EventSagaDriven.Api.Middleware;
using EventSagaDriven.Application.Account.Interfaces;
using EventSagaDriven.Domain.Entities.Account.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EventSagaDriven.Api.Controllers.UseCase.Account.GetByEmail;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IGetAccountByEmail _useCase;
    public AccountController(IGetAccountByEmail useCase)
    {
        _useCase = useCase;
    }

    [HttpGet("email")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseAccountByEmail))]
    [ProducesResponseType(typeof(CustomErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CustomErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAccountByIdAsync([Required] string email,
    CancellationToken cancellationToken)
    {
        var account = await _useCase.ExecuteAsync(Email.Create(email), cancellationToken);
        if (account == null)
        {
            return NotFound();
        }

        return StatusCode((int)HttpStatusCode.OK, ResponseAccountByEmail.Map(account));
    }
}
