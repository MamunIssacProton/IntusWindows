using System;
namespace IntusWindows.Sales.Order.Api.Commands.Update;

public class ChangeDimensionInOrderCommand
{
	public required Guid OrderId { get; set; }

	public required Guid WindowId { get; set; }

	public required string ExistDimensionId { get; set; }

	public required string DisiredDimensionId { get; set; }

}

