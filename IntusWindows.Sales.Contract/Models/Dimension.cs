using System;
using IntusWindows.Sales.Contract.Enums;

namespace IntusWindows.Sales.Contract.Models;

public class Dimension
{
    public  string Title { get; set; }

    public decimal Height { get; set; }

    public decimal Width { get; set; }

    public ElementType elementType { get; set; }
}

