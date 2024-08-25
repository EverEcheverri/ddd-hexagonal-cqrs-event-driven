using EventSagaDriven.Domain.Entities.Account.Commands;
using EventSagaDriven.Domain.Entities.Account.Enums;
using EventSagaDriven.Domain.Entities.Account.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace EventSagaDriven.Api.Controllers.UseCase.Account.Create;

public class RequestCreateAccount
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public Guid CityId { get; set; }

    [Required]
    public string Mobile { get; set; }

    [Required]
    public int AccountType { get; set; }

    public CreateAccountCommand ToCreateAccountCommand()
    {
        return new CreateAccountCommand
        {
            Id = Id,
            Email = Domain.Entities.Account.ValueObjects.Email.Create(Email),
            UserName = Domain.Entities.Account.ValueObjects.UserName.Create(UserName),
            Mobile = Domain.Entities.Account.ValueObjects.Mobile.Create(Mobile),
            AccountType = (AccountType)AccountType,
            CityId = CityId
        };
    }
}
