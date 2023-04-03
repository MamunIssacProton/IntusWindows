using System;
namespace IntusWindows.Sales.Order.Api.Commands.Update;

public class ChangeDimensionFromOrderCommand
{
    public required Guid OrderId { get; set; }

    public required Guid WindowId { get; set; }

    public required string CurrentDimensionId { get; set; }

    public required string NewDimensionId { get; set; }

}

