using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.Element;

public class ElementNameChanged : IDomainEvent
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

}

