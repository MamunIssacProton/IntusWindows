using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.Dimension;

public class WidthUpdated : IDomainEvent
{
    public string Id { get; set; }

    public decimal Width { get; set; }
}

