using System;
namespace IntusWindows.Sales.Contract.Models;

public record DeleteWindowFromOrder(Guid OrderId, Guid WindowId);

