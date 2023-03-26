using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Events.Window;

public class QuantityOfWinodwsUpdated : IDomainEvent
{
    public Guid Id { get; set; }

    public int Quantity { get; set; }
}

