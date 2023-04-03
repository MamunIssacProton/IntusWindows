namespace IntusWindows.Sales.Order.Domain.Exceptions;

public class InvalidElementStateException : Exception
{
    public InvalidElementStateException(string message) : base(message)
    {
    }
}

