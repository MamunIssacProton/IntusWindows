using IntusWindows.Sales.Order.Domain.Enums;
using IntusWindows.Sales.Order.Domain.Utils;

namespace IntusWindows.Sales.Order.Domain.ValueObjects;

public class Width
{
    public decimal Value { get; private set; }

    protected Width(decimal value) => this.Value = value;

    public static Width Create(ElementType elementType, decimal value)
    {
        ValidatorFactory.ValidateWidth(elementType, nameof(Width), value);

        return new Width(value);
    }

    public static implicit operator decimal(Width width) => width.Value;


}

