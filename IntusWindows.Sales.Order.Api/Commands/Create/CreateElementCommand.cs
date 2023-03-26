namespace IntusWindows.Sales.Order.Api.Commands.Create;

public class CreateElementCommand
	{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string DimensionId { get; set; }
}

