using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.Dimension;

public class HeightUpdated : IDomainEvent
{
    public string Id { get; set; }

    public decimal Height { get; set; }
}

