using System;
using IntusWindows.Sales.Contract.DTOs;

namespace IntusWindows.Sales.Contract.Models;

public class WindowTreeNode
{
	public string WindowName { get; set; }

	public Guid WindowId { get; set; }

	public List<ElementNode> elements { get; set; }

	public bool IsExpanded { get; set; }

}

