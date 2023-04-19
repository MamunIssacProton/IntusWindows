using System;
namespace IntusWindows.Sales.Order.Api.Commands.Update;

public class ChangeDimensionFromOrderCommand
{
    public Guid OrderId { get; set; }

    public Guid WindowId { get; set; }

    public string? CurrentDimensionId { get; set; }

    public string? NewDimensionId { get; set; }

}

