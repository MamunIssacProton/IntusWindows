using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.Window;

public class WindowCreated : IDomainEvent
{
    public Guid Id { get; set; }

}

