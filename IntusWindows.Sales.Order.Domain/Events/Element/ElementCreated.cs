using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.Element;

public class ElementCreated : IDomainEvent
{
    public Guid Id { get; set; }

}

