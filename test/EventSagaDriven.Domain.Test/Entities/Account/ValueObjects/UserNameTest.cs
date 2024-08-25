using EventSagaDriven.Domain.Entities.Account.Exceptions;
using EventSagaDriven.Domain.Entities.Account.ValueObjects;

namespace EventSagaDriven.Domain.Test.Entities.Account.ValueObjects;


public class UserNameTest
{
    [Fact]
    public void UserName_Create_ValueObject()
    {
        var userName = UserName.Create("developer_one");

        Assert.NotNull(userName);
        Assert.Equal("developer_one", userName.Value);
    }

    [Fact]
    public void UserName_Empty_Throws_UserNameNullOrEmptyException()
    {
        var exception = Assert.Throws<UserNameNullOrEmptyException>(() => UserName.Create(string.Empty));
        Assert.Equal("User name is null or empty", exception.Message);
    }
}
