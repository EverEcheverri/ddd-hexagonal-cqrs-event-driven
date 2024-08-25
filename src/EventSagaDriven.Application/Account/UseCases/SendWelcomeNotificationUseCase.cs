using EventSagaDriven.Domain.Entities.Account.Events;
using MediatR;

namespace EventSagaDriven.Application.Account.UseCases;

public class SendWelcomeNotificationUseCase : INotificationHandler<AccountCreated>
{

    public SendWelcomeNotificationUseCase()
    {

    }

    public async Task Handle(AccountCreated notification, CancellationToken cancellationToken)
    {
        await Task.Delay(1000);
    }
}