using System;
namespace IntusWindows.Sales.Contract.Exceptions;

public class NotFoundException:Exception
{
	public NotFoundException(string message):base(message)
	{
	}
}

