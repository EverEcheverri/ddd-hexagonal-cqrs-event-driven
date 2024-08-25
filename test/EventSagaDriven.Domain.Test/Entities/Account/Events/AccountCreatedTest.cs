using EventSagaDriven.Domain.Entities.Account.Enums;
using EventSagaDriven.Domain.Entities.Account.Events;

namespace EventSagaDriven.Domain.Test.Entities.Account.Events;

public class AccountCreatedTest
{
    [Fact]
    public void AccountCreated_Successfully()
    {
        // Arrange

        // Act
        var accountCreated = new AccountCreated
        {
            Id = Guid.Parse("b2181377-6a51-446e-afb6-07f1402834e3"),
            Email = "developer_one@yopmail.com",
            UserName = "developer_one",
            Mobile = "3110002233",
            AccountType = (AccountType)1,
            CityId = Guid.Parse("5ebf0600-c390-4b16-945d-eb0e734cf51c")
        };

        // Assert
        Assert.NotNull(accountCreated);
        Assert.Equal(Guid.Parse("b2181377-6a51-446e-afb6-07f1402834e3"), accountCreated.Id);
        Assert.Equal("developer_one@yopmail.com", accountCreated.Email);
        Assert.Equal("developer_one", accountCreated.UserName);
        Assert.Equal("3110002233", accountCreated.Mobile);
        Assert.Equal((AccountType)1, accountCreated.AccountType);
        Assert.Equal(Guid.Parse("5ebf0600-c390-4b16-945d-eb0e734cf51c"), accountCreated.CityId);
    }
}
