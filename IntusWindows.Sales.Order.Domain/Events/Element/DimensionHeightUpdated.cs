using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.Element;

public class DimensionHeightUpdated : IDomainEvent
{
    public string Id { get; set; }

    public decimal Height { get; set; }
}

