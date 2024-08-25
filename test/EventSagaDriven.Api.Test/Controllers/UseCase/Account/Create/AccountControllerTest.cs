using EventSagaDriven.Api.Controllers.UseCase.Account.Create;
using EventSagaDriven.Application.Account.Interfaces;
using EventSagaDriven.Domain.Entities.Account.Commands;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Net;

namespace EventSagaDriven.Api.Test.Controllers.UseCase.Account.Create;

public class AccountControllerTest
{
    private readonly ICreateAccount _createAccountMock;
    private readonly AccountController _controller;

    public AccountControllerTest()
    {
        _createAccountMock = Substitute.For<ICreateAccount>();
        _controller = new AccountController(_createAccountMock);
    }

    [Fact]
    public async Task CreateAccountAsync_ValidRequest_ReturnsCreated()
    {
        // Arrange
        var request = new RequestCreateAccount
        {
            Id = Guid.Parse("b2181377-6a51-446e-afb6-07f1402834e3"),
            Email = "developer_one@yopmail.com",
            UserName = "developer_one",
            Mobile = "3110002233",
            AccountType = 1,
            CityId = Guid.Parse("5ebf0600-c390-4b16-945d-eb0e734cf51c")
        };
        var command = request.ToCreateAccountCommand();

        _createAccountMock.ExecuteAsync(command, CancellationToken.None)
                          .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.CreateAccountAsync(request, CancellationToken.None);

        // Assert
        Assert.IsType<ObjectResult>(result);
        var createdResult = result as ObjectResult;
        Assert.Equal((int)HttpStatusCode.Created, createdResult?.StatusCode);
        Assert.Equal(request.Email, createdResult?.Value);

        // Verify service call
        await _createAccountMock.Received(1).ExecuteAsync(Arg.Any<CreateAccountCommand>(), CancellationToken.None);
    }
}
