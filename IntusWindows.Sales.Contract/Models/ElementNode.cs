using System;
namespace IntusWindows.Sales.Contract.Models;

//public record ElementNode(Guid windowId, Guid elementId, string elementName=default, bool IsExpanded=false, string dimensionId=default) { };

public class ElementNode
{
    public Guid WindowId { get; set; }
    public Guid ElementId { get; set; }
    public string ElementName { get; set; }
    public bool IsExpanded { get; set; }
    public string DimensionId { get; set; }

    public ElementNode(Guid windowId, Guid elementId, string elementName = default, bool isExpanded = false, string dimensionId = default)
    {
        WindowId = windowId;
        ElementId = elementId;
        ElementName = elementName;
        IsExpanded = isExpanded;
        DimensionId = dimensionId;
    }
}
