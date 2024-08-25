using EventSagaDriven.Domain.Entities.Account.Enums;
using EventSagaDriven.Domain.Entities.Account.Events;
using EventSagaDriven.Domain.Entities.Account.ValueObjects;
using EventSagaDriven.Infrastructure.EntityFramework;
using EventSagaDriven.Infrastructure.EntityFramework.Account.Repositories.Sqlite;
using EventSagaDriven.Infrastructure.EntityFramework.Interfaces;
using EventSagaDriven.Test.Data.Account;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace EventSagaDriven.Infrastructure.Test.EntityFramework.Account.Repositories.Sqlite;

public class SqliteAccountRepositoryTest
{
    private readonly DbContextOptions<SqliteEventSagaDrivenContext> _options;
    private readonly ILogger<SqliteEventSagaDrivenContext> _loggerMock;
    private IPublisher _publisherMock;
    private readonly IConfiguration _configurationMock;
    private IInfrastructureAccountRepository _accountRepository;

    public SqliteAccountRepositoryTest()
    {
        _options = new DbContextOptionsBuilder<SqliteEventSagaDrivenContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
      .Options;

        _loggerMock = Substitute.For<ILogger<SqliteEventSagaDrivenContext>>();
        _publisherMock = Substitute.For<IPublisher>();

        var mockConnectionStringsSection = Substitute.For<IConfigurationSection>();
        mockConnectionStringsSection["CosmosConnectionString"]
            .Returns("AccountEndpoint=https://localhost/;AccountKey=abc");

        _configurationMock = Substitute.For<IConfiguration>();
        _configurationMock.GetSection("ConnectionStrings")
            .Returns(mockConnectionStringsSection);

        string connectionString = _configurationMock["ConnectionStrings:CosmosConnectionString"];
    }

    [Fact]
    public async Task InfrastructureAccountRepository_GetByEmailAsync_Returns_Account_WhenExists()
    {
        // Arrange
        var accountOne = AccountMother.Create();
        var accountTwo = AccountMother.Create(id: "140c7396-cb76-45ea-88c5-e709702dd927", email: "developer_two@yopmail.com");

        await using var context = new SqliteEventSagaDrivenContext(_loggerMock, _configurationMock, _options, _publisherMock);
        await context.Accounts.AddAsync(accountOne);
        await context.Accounts.AddAsync(accountTwo);
        await context.SaveChangesAsync();

        await using var contextAssert = new SqliteEventSagaDrivenContext(_loggerMock, _configurationMock, _options, _publisherMock);
        _accountRepository = new SqliteAccountRepository(contextAssert);

        // Act
        var account = await _accountRepository.GetByEmailAsync(accountOne.Email, CancellationToken.None);

        // Assert File
        Assert.Equal(Guid.Parse("b2181377-6a51-446e-afb6-07f1402834e3"), account.Id);
        Assert.Equal("developer_one@yopmail.com", account.Email.Value);
        Assert.Equal("developer_one", account.UserName.Value);
        Assert.Equal("3110002233", account.Mobile.Value);
        Assert.Equal((AccountType)1, account.AccountType);
        Assert.Equal(Guid.Parse("5ebf0600-c390-4b16-945d-eb0e734cf51c"), account.CityId);
    }

    [Fact]
    public async Task InfrastructureAccountRepository_GetByEmailAsync_Returns_Null_WhenDoesNotExists()
    {
        // Arrange

        await using var contextAssert = new SqliteEventSagaDrivenContext(_loggerMock, _configurationMock, _options, _publisherMock);
        _accountRepository = new SqliteAccountRepository(contextAssert);

        // Act
        var account = await _accountRepository.GetByEmailAsync(Email.Create("developer_two@yopmail.com"), CancellationToken.None);

        // Assert File
        Assert.Null(account);
    }

    [Fact]
    public async Task InfrastructureAccountRepository_SaveAsync()
    {
        // Arrange
        var account = AccountMother.Create();

        object? accountCreated = null;
        _publisherMock.When(a => a.Publish(Arg.Any<object>(), CancellationToken.None))
            .Do(call => accountCreated = call.ArgAt<object>(0));

        await using var context = new SqliteEventSagaDrivenContext(_loggerMock, _configurationMock, _options, _publisherMock);
        _accountRepository = new SqliteAccountRepository(context);

        // Act
        await _accountRepository.SaveAsync(account, CancellationToken.None);

        // Assert Account Created
        await using var contextAssert = new SqliteEventSagaDrivenContext(_loggerMock, _configurationMock, _options, _publisherMock);
        var created = await contextAssert.Accounts.FirstOrDefaultAsync(x => x.Id == account.Id);

        Assert.NotNull(created);
        Assert.Equal(Guid.Parse("b2181377-6a51-446e-afb6-07f1402834e3"), created.Id);
        Assert.Equal("developer_one@yopmail.com", created.Email.Value);
        Assert.Equal("developer_one", created.UserName.Value);
        Assert.Equal("3110002233", created.Mobile.Value);
        Assert.Equal((AccountType)1, created.AccountType);
        Assert.Equal(Guid.Parse("5ebf0600-c390-4b16-945d-eb0e734cf51c"), created.CityId);

        // Assert event publishing
        await _publisherMock.Received().Publish(Arg.Any<object>());
        Assert.IsType<AccountCreated>(accountCreated);

        var eventPublished = (AccountCreated)accountCreated;
        Assert.Equal(Guid.Parse("b2181377-6a51-446e-afb6-07f1402834e3"), eventPublished.Id);
        Assert.Equal("developer_one@yopmail.com", eventPublished.Email);
        Assert.Equal("developer_one", eventPublished.UserName);
        Assert.Equal("3110002233", eventPublished.Mobile);
        Assert.Equal((AccountType)1, eventPublished.AccountType);
        Assert.Equal(Guid.Parse("5ebf0600-c390-4b16-945d-eb0e734cf51c"), eventPublished.CityId);
    }
}
