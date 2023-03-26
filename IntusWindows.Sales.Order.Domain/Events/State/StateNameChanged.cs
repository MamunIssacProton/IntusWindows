using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.State;

public class StateNameChanged:IDomainEvent
{
	public required Guid Id { get; set; }

	public required string Name { get; set; }
}

