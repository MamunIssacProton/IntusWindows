using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.State;

public class StateCreated:IDomainEvent
{
	public required Guid Id { get; set; }

}

