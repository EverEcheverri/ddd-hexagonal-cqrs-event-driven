namespace EventSagaDriven.Domain.SharedKernel.Events;

public class Header
{
    public readonly string ServiceId = "Account";
    public Header()
    {
        MessageId = Guid.NewGuid().ToString();
        EventGeneratedDate = DateTime.Now;
    }
    public string MessageId { get; }
    public string EventType { get; set; }
    public string Subject { get; set; }
    public DateTime EventGeneratedDate { get; }
}
