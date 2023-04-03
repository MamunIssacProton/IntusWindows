using System;
using IntusWindows.Sales.Contract.Enums;

namespace IntusWindows.Sales.Contract.Models;

public record Dimension(string Title, decimal Height, decimal Width, ElementType elementType);
