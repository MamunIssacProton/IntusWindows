using System;
using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.Order;

public class AssignedStateToOrder:IDomainEvent
{
	public required Guid Id { get; set; }

	public required string State { get; set; }
}

