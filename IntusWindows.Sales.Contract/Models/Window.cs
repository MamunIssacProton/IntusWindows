using System;
namespace IntusWindows.Sales.Contract.Models;

public record Window (Guid Id, string Title, int QuantityOfWindows, List<Guid> ElementIds);

