namespace IntusWindows.Sales.Order.Domain.ValueObjects;

public class WindowId
{
    public Guid value { get; private set; }

    private WindowId(Guid value) => this.value = value;

    public static WindowId Create(Guid value)
    {
        if (value == default)
            throw new ArgumentException("Window id should not be default");

        if (value == Guid.Empty)
            throw new Exception("Window id should not be empty");


        return new WindowId(value);
    }

    public static implicit operator Guid(WindowId windowId) => windowId.value;

}

