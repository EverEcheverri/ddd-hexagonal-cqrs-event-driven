using EventSagaDriven.Application.Account.UseCases;
using EventSagaDriven.Domain.Entities.Account.Enums;
using EventSagaDriven.Domain.Entities.Account.Repositories;
using EventSagaDriven.Test.Data.Commands;
using NSubstitute;

namespace EventSagaDriven.Application.Test.Account.UseCases;

public class CreateAccountUseCaseTest
{
    private readonly CreateAccountUseCase _useCase;
    private readonly IAccountRepository _accountRepositoryMock;


    public CreateAccountUseCaseTest()
    {
        _accountRepositoryMock = Substitute.For<IAccountRepository>();
        _useCase = new CreateAccountUseCase(_accountRepositoryMock);
    }

    [Fact]
    public async Task CreateUserUseCase_ExecuteAsync_Successfully()
    {
        // Arrange
        var command = CreateAccountCommandMother.Create();

        _accountRepositoryMock.GetByEmailAsync(command.Email, CancellationToken.None)
            .Returns(null as Domain.Entities.Account.Account);

        Domain.Entities.Account.Account? account = null;
        _accountRepositoryMock.When(a => a.SaveAsync(Arg.Any<Domain.Entities.Account.Account>(), CancellationToken.None))
            .Do(call => account = call.ArgAt<Domain.Entities.Account.Account>(0));

        // Act
        await _useCase.ExecuteAsync(command, CancellationToken.None);

        // Asserts Mocks
        await _accountRepositoryMock.Received().GetByEmailAsync(command.Email, CancellationToken.None);
        await _accountRepositoryMock.Received().SaveAsync(account!, CancellationToken.None);

        // Asserts callback
        Assert.NotNull(account);
        Assert.Equal(Guid.Parse("b2181377-6a51-446e-afb6-07f1402834e3"), account.Id);
        Assert.Equal("developer_one@yopmail.com", account.Email.Value);
        Assert.Equal("developer_one", account.UserName.Value);
        Assert.Equal("3110002233", account.Mobile.Value);
        Assert.Equal((AccountType)1, account.AccountType);
        Assert.Equal(Guid.Parse("5ebf0600-c390-4b16-945d-eb0e734cf51c"), account.CityId);
    }
}
