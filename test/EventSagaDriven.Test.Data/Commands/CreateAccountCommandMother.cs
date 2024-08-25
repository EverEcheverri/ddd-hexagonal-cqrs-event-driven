using EventSagaDriven.Domain.Entities.Account.Commands;
using EventSagaDriven.Domain.Entities.Account.Enums;
using EventSagaDriven.Domain.Entities.Account.ValueObjects;

namespace EventSagaDriven.Test.Data.Commands;

public class CreateAccountCommandMother
{
    public static CreateAccountCommand Create(
    string id = "b2181377-6a51-446e-afb6-07f1402834e3",
    string email = "developer_one@yopmail.com",
    string userName = "developer_one",
    string mobile = "3110002233",
    int accountType = 1,
    string cityId = "5ebf0600-c390-4b16-945d-eb0e734cf51c")
    {
        return new CreateAccountCommand
        {
            Id = Guid.Parse(id),
            Email = Email.Create(email),
            UserName = UserName.Create(userName),
            Mobile = Mobile.Create(mobile),
            AccountType = (AccountType)accountType,
            CityId = Guid.Parse(cityId)
        };
    }
}