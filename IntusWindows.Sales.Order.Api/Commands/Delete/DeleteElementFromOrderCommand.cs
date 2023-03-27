namespace IntusWindows.Sales.Order.Api.Commands.Delete;

public class DeleteElementFromOrderCommand
{
	public required Guid Id { get; set; }

	public required Guid WindowId { get; set; }

	public required Guid ElementId { get; set; }

}

