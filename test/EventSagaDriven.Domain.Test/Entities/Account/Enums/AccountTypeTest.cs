using EventSagaDriven.Domain.Entities.Account.Enums;

namespace EventSagaDriven.Domain.Test.Entities.Account.Enums;

public class AccountTypeTest
{
    [Fact]
    public void MediaStatus_Creates_PendingToUpload()
    {
        Assert.Equal(1, (int)AccountType.Seller);
        Assert.Equal(2, (int)AccountType.Buyer);
    }
}
