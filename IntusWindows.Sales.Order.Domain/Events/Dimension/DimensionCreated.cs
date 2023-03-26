using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.Dimension;

public class DimensionCreated : IDomainEvent
{
    public string Id { get; set; }
}

