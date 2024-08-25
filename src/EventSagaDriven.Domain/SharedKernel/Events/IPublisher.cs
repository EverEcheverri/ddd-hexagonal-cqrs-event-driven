namespace EventSagaDriven.Domain.SharedKernel.Events
{
    public interface IPublisher
    {
        Task PublishAsync(IReadOnlyList<IDomainEvent> events, CancellationToken cancellationToken);
    }
}
