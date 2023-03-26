using IntusWindows.Sales.Order.Domain.Enums;

namespace IntusWindows.Sales.Order.Api.Commands.Create;

public class CreateDimensionCommand
{


    public required string Title { get; set; }

    public decimal Height { get; set; }

    public decimal Width { get; set; }

    public ElementType elementType { get; set; }
}

