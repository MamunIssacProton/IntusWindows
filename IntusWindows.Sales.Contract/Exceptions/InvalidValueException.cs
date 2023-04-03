namespace IntusWindows.Sales.Contract.Exceptions;

public class InvalidValueException : Exception
{
    public InvalidValueException(string property, string message) : base($"{property} , {message}")
    {

    }
}

