using System;
namespace IntusWindows.Sales.Contract.Models;

public record DeleteElementFromOrder(Guid Id, Guid WindowId, Guid ElementId);

