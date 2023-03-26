using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.Window;

public class ElementAddedToWindow : IDomainEvent
{
    public Guid WindowId { get; set; }

    public Guid ElementId { get; set; }

}

