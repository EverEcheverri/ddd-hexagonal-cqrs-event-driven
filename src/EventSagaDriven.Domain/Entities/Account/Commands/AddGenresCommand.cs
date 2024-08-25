using EventSagaDriven.Domain.Entities.Account.ValueObjects;

namespace EventSagaDriven.Domain.Entities.Account.Commands;

public class AddGenresCommand
{
    public Email Email { get; set; }
    public List<Guid> GenreIds { get; set; }
}
