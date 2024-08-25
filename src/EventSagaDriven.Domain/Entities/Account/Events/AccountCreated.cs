namespace EventSagaDriven.Domain.Entities.Account.Events;

using EventSagaDriven.Domain.Entities.Account.Enums;
using EventSagaDriven.Domain.SharedKernel.Events;

public class AccountCreated : DomainEvent
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Mobile { get; set; }
    public AccountType AccountType { get; set; }
    public Guid CityId { get; set; }
}
