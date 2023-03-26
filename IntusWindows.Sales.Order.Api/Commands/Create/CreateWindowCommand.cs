namespace IntusWindows.Sales.Order.Api.Commands.Create;

public class CreateWindowCommand
	{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public int QuantityOfWindows { get; set; }

    public List<Guid> ElementIds { get; set; }
}

