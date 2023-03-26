using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.Order;

public class OrderNameChanged:IDomainEvent
{
	public required Guid Id { get; set; }

	public required string Name { get; set; }
}

