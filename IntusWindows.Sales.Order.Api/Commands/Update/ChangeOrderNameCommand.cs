namespace IntusWindows.Sales.Order.Api.Commands.Update;

public class ChangeOrderNameCommand
{
	public required Guid Id { get; set; }

	public required string OrderName { get; set; }

}

