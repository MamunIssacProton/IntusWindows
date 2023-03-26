using System;
namespace IntusWindows.Sales.Order.Domain.Exceptions;

public class NotFoundException:Exception
{
	public NotFoundException(string message):base(message)
	{
	}
}

