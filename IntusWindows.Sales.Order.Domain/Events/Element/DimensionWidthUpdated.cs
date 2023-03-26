using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.Element;

public class DimensionWidthUpdated : IDomainEvent
{
    public string Id { get; set; }

    public decimal Width { get; set; }
}

