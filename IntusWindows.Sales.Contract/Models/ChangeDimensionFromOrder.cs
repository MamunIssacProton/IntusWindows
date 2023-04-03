using System;
namespace IntusWindows.Sales.Contract.Models;

public record ChangeDimensionFromOrder(Guid OrderId, Guid WindowId, string CurrentDimensionId, string NewDimensionId);

