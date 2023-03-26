namespace IntusWindows.Sales.Order.Api.Commands.Update;

public class UpdateStateCommand
{
	public required Guid Id { get; set; }

	public required string Name { get; set; }
}

