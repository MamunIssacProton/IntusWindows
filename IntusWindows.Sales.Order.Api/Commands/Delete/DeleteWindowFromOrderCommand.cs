namespace IntusWindows.Sales.Order.Api.Commands.Delete;

public class DeleteWindowFromOrderCommand
{
	public required Guid OrderId { get; set; }

	public required Guid WindowId { get; set; }



}

