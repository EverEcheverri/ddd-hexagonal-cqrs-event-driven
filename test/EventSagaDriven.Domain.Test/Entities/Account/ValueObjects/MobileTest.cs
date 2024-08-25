using EventSagaDriven.Domain.Entities.Account.Exceptions;
using EventSagaDriven.Domain.Entities.Account.ValueObjects;

namespace EventSagaDriven.Domain.Test.Entities.Account.ValueObjects;


public class MobileTest
{
    [Fact]
    public void Mobile_Create_ValueObject()
    {
        var mobile = Mobile.Create("1111111111");

        Assert.NotNull(mobile);
        Assert.Equal("1111111111", mobile.Value);
    }

    [Fact]
    public void Mobile_Empty_Throws_MobileNullOrEmptyException()
    {
        var exception = Assert.Throws<MobileNullOrEmptyException>(() => Mobile.Create(string.Empty));
        Assert.Equal("Mobile is null or empty", exception.Message);
    }

    [Fact]
    public void Mobile_Empty_Throws_MobileNoValidNumberException()
    {
        var mobile = $"{new string('a', 10)}";
        var exception = Assert.Throws<MobileNoValidNumberException>(() => Mobile.Create(mobile));
        Assert.Equal("Mobile number is not valid", exception.Message);
    }
}
