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

		public required string Title { get; set; }

		public required int QuantityOfWindows { get; set; }

		public required List<Guid> ElementIds { get; set; }

	}

	public class Element
	{
		public Guid Id { get; set; }

		public required string Name { get; set; }

		public required string DimensionId { get; set; }

	}


}

