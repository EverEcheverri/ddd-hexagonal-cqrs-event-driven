using EventSagaDriven.Domain.Entities.Account.Enums;
using EventSagaDriven.Test.Data.Commands;

namespace EventSagaDriven.Domain.Test.Entities.Account.Commands;

public class CreateAccountCommandTest
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void FileInfoStorage_IsFailedOrAborted(int accountType)
    {
        // Act
        var createAccountCommand = CreateAccountCommandMother.Create(accountType: accountType);

        // Assert
        Assert.Equal(Guid.Parse("b2181377-6a51-446e-afb6-07f1402834e3"), createAccountCommand.Id);
        Assert.Equal("developer_one@yopmail.com", createAccountCommand.Email.Value);
        Assert.Equal("developer_one", createAccountCommand.UserName.Value);
        Assert.Equal("3110002233", createAccountCommand.Mobile.Value);
        Assert.Equal((AccountType)accountType, createAccountCommand.AccountType);
        Assert.Equal(Guid.Parse("5ebf0600-c390-4b16-945d-eb0e734cf51c"), createAccountCommand.CityId);
    }
}
