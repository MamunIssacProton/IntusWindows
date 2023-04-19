using System;
namespace IntusWindows.Sales.Order.Api.Commands.Update;

public class ChangeStateInOrderCommand
{
	public Guid OrderId { get; set; }

	public Guid StateId { get; set; } 
}

