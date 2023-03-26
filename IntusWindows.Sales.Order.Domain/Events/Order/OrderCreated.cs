using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.Order;

public class OrderCreated:IDomainEvent
{
	public required Guid Id { get; set; }
}

