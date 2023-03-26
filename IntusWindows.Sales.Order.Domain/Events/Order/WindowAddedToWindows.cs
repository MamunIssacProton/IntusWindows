using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.Order;

public class WindowAddedToWindows: IDomainEvent
{
	public Guid OrderId { get; set; }

	public Guid WindowId { get; set; }

}

