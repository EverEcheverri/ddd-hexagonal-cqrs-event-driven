using EventSagaDriven.Domain.SharedKernel.Events;

namespace EventSagaDriven.Domain.Entities.Account.Events;

internal static class AccountRaiseDomainEvent
{
    internal static void AccountCreatedEvent(this Account account)
    {
        account.RaiseDomainEvent(new Event<AccountCreated>
        {
            Body = new AccountCreated
            {
                Id = account.Id,
                Email = account.Email.Value,
                UserName = account.UserName.Value,
                Mobile = account.Mobile.Value,
                AccountType = account.AccountType,
                CityId = account.CityId
            }
        });
    }
}
