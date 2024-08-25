using EventSagaDriven.Domain.Entities.Account.Enums;
using EventSagaDriven.Domain.Entities.Account.ValueObjects;

namespace EventSagaDriven.Domain.Entities.Account.Commands;

public class CreateAccountCommand
{
    public Guid Id { get; set; }
    public Email Email { get; set; }
    public UserName UserName { get; set; }
    public Mobile Mobile { get; set; }
    public AccountType AccountType { get; set; }
    public Guid CityId { get; set; }
}
