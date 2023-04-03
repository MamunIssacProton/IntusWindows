using System;
namespace IntusWindows.Sales.Contract.Models;

public record Order (Guid Id, string OrderName, Guid StateId, List<Guid> WindowsIds);


