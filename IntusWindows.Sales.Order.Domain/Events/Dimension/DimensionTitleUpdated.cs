using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.Dimension;

public class DimensionTitleUpdated : IDomainEvent
{
    public string Id { get; set; }

    public string Title { get; set; }

}

