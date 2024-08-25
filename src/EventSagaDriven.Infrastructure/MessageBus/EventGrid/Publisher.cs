using Azure.Messaging.EventGrid;
using EventSagaDriven.Domain.SharedKernel.Events;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace EventSagaDriven.Infrastructure.MessageBus.EventGrid;

public class Publisher : IPublisher
{
    private const string DataVersion = "1.0";
    private const string TopicEndpoint = "AzureAd:TopicEndpoint";
    private const string TopicAccessKey = "AzureAd:AzureKeyCredential";
    private const string BodyProperty = "Body";
    private const string HeaderProperty = "Header";

    private readonly EventGridPublisherClient _publisherClient;
    public Publisher([NotNull] IConfiguration configuration)
    {
        var topicEndpointValue = configuration[TopicEndpoint];
        var topicAccessKeyValue = configuration[TopicAccessKey];
        _publisherClient = new EventGridPublisherClient(
            new Uri(topicEndpointValue),
            new Azure.AzureKeyCredential(topicAccessKeyValue));
    }

    public async Task PublishAsync(IReadOnlyList<IDomainEvent> events, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (events == null || !events.Any())
        {
            return;
        }

        var eventridEvents = new List<EventGridEvent>();

        foreach (var @event in events)
        {
            var eventTypeClass = @event.GetType();
            if (eventTypeClass != null)
            {
                if (eventTypeClass.IsGenericType && eventTypeClass.GetGenericTypeDefinition() == typeof(Event<>))
                {
                    var eventData = eventTypeClass?.GetProperty(BodyProperty)?.GetValue(@event);
                    var eventHeader = eventTypeClass?.GetProperty(HeaderProperty)?.GetValue(@event) as Header;

                    eventridEvents.Add(new EventGridEvent(
                        eventHeader?.Subject,
                        eventHeader?.EventType,
                        DataVersion,
                        eventData));
                }
            }
        }

        await _publisherClient.SendEventsAsync(eventridEvents, cancellationToken);
    }
}
