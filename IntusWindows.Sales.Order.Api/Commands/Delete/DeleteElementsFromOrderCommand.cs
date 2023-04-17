using IntusWindows.Sales.Contract.Models;

namespace IntusWindows.Sales.Order.Api.Commands.Delete;

public class DeleteElementsFromOrderCommand
{
    public required Guid OrderId { get; set; }

    public required List<ElementNode> Elements { get; set; }
}

