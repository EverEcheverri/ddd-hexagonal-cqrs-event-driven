
using EventSagaDriven.Domain.Entities.Account.Exceptions;
using EventSagaDriven.Domain.Entities.Account.ValueObjects;

namespace EventSagaDriven.Domain.Test.Entities.Account.ValueObjects;

public class EmailTest
{
    [Fact]
    public void Email_Create_ValueObject()
    {
        var email = Email.Create("developer_one@yopmail.com");

        Assert.NotNull(email);
        Assert.Equal("developer_one@yopmail.com", email.Value);
    }

    [Fact]
    public void Email_Empty_Throws_EmailNullOrEmptyException()
    {
        var exception = Assert.Throws<EmailNullOrEmptyException>(() => Email.Create(string.Empty));
        Assert.Equal("Email is null or empty", exception.Message);
    }

    [Fact]
    public void Email_Throws_EmailMaxLengthException()
    {
        var email = $"{new string('a', 255)}";
        var exception = Assert.Throws<EmailMaxLengthException>(() => Email.Create(email));
        Assert.Equal("Email max length exceeded 254 characters", exception.Message);
    }

    [Theory]
    [InlineData("developer_one")]
    [InlineData("developer_one@yopmail")]
    [InlineData("developer_one_@@yopmail.com")]
    public void Email_Throws_EmailInvalidPatternException(string email)
    {
        var exception = Assert.Throws<EmailInvalidPatternException>(() => Email.Create(email));
        Assert.Equal("Email invalid pattern", exception.Message);
    }
}
