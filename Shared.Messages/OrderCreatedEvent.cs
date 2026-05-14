namespace Shared.Messages;

public class OrderCreatedEvent
{
    public Guid OrderId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
}