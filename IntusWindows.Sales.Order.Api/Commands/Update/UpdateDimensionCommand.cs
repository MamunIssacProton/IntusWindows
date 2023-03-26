namespace IntusWindows.Sales.Order.Api.Commands.Update;

public class UpdateDimensionCommand
{
    public required string Id { get; set; }

    public required decimal Height { get; set; }

    public required decimal Width { get; set; }

    public required string Title { get; set; }
}

