using EventSagaDriven.Domain.Entities.Account.Commands;
using System.ComponentModel.DataAnnotations;

namespace EventSagaDriven.Api.Controllers.UseCase.Account.AddGenres;

public class RequestAddGenres
{
    [Required]
    public string Email { get; set; }

    [Required]
    public List<Guid> GenreIds { get; set; }

    internal AddGenresCommand ToAddGenresCommand()
    {
        return new AddGenresCommand
        {
            Email = Domain.Entities.Account.ValueObjects.Email.Create(Email),
            GenreIds = GenreIds
        };
    }
}
