using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.Element;

public class DimensionChanged : IDomainEvent
{
    public string Id { get; set; }

    public decimal Height { get; set; }

    public decimal Width { get; set; }


}

