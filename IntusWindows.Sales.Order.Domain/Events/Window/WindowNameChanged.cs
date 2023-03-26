using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.Window;

public class WindowNameChanged : IDomainEvent
{
    public Guid Id { get; set; }

    public required string Name { get; set; }
}

