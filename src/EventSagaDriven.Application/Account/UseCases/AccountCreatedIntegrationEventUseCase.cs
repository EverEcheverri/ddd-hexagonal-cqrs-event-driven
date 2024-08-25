using EventSagaDriven.Domain.Entities.Account.Events;
using EventSagaDriven.Domain.SharedKernel.Events;
using MediatR;

namespace EventSagaDriven.Application.Account.UseCases;

public class AccountCreatedIntegrationEventUseCase : INotificationHandler<AccountCreated>
{
    private readonly Domain.SharedKernel.Events.IPublisher _publisher;
    private const string AggregateName = "Account";
    private const string CreatedSubject = "Created";
    public AccountCreatedIntegrationEventUseCase(Domain.SharedKernel.Events.IPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Handle(AccountCreated accountCreated, CancellationToken cancellationToken)
    {
        var accountCreatedEvent = new Event<AccountCreated>
        {
            Header = new Header
            {
                EventType = nameof(AccountCreated),
                Subject = CreatedSubject
            },
            Body = new AccountCreated
            {
                Id = accountCreated.Id,
                Email = accountCreated.Email,
                UserName = accountCreated.UserName,
                Mobile = accountCreated.Mobile,
                AccountType = accountCreated.AccountType,
                CityId = accountCreated.CityId,
                IsPublished = true
            },
        };

        var eventToPublish = new List<IDomainEvent> { accountCreatedEvent };

        await _publisher.PublishAsync(eventToPublish, cancellationToken);
    }
}