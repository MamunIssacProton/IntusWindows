using System;
using IntusWindows.Sales.Contract.Enums;

namespace IntusWindows.Sales.Contract.Models.Map;

public static class Mapper
{
    public class State
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }
    }

    public class Dimension
    {


        public required string Title { get; set; }

        public required decimal Height { get; set; }

        public required decimal Width { get; set; }

        public required ElementType ElementType { get; set; }
    }

    public class Window
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public int QuantityOfWindows { get; set; }

        public List<Guid> ElementIds { get; set; }

    }

    public class Element
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? DimensionId { get; set; }

    }

    public class Order
    {
        public Guid Id { get; set; }

        public string? OrderName { get; set; }

        public Guid StateId { get; set; }

        public List<Guid> Windows { get; set; }
    }

    public class ChangeDimensionFormOrder
    {
        public Guid OrderId { get; set; }

        public Guid WindowId { get; set; }

        public string? CurrentDimensionId { get; set; }

        public string? NewDimensionId { get; set; }
    }

    public class ChangeOrderName
    {
        public Guid Id { get; set; }

        public string? OrderName { get; set; }
    }

    public class ChangeStateInOrder
    {
        public Guid OrderId { get; set; }

        public Guid StateId { get; set; }
    }

    public class DeleteOrder
    {
        public Guid OrderId { get; set; }
    }

    public class DeleteWindowsFromOrder
    {
        public Guid OrderId { get; set; }

        public List<Guid> WindowIds { get; set; }

    }
    public class DeleteElementsFromOrdr
    {
        public Guid OrderId { get; set; }

        public List<ElementNode> Elements { get; set; }

    }
    public class UpdateDimension
    {
        public string? Id { get; set; }

        public decimal Height { get; set; }

        public decimal Width { get; set; }
    }

    public class UpdateState
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
    }
}

