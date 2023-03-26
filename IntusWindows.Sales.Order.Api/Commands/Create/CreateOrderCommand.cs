using System;
namespace IntusWindows.Sales.Order.Api.Commands.Create;

public class CreateOrderCommand
{
	public required Guid Id { get; set; }

	public required string OrderName { get; set; }

	public required Guid StateId { get; set; }

	public required List<Guid> WindowIds { get; set; }
}

