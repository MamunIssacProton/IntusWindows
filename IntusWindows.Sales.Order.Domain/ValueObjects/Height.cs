using IntusWindows.Sales.Order.Domain.Enums;
using IntusWindows.Sales.Order.Domain.Utils;

namespace IntusWindows.Sales.Order.Domain.ValueObjects;

public class Height
{
    public decimal Value { get; private set; }

    protected Height(decimal value) => this.Value = value;

    public static Height Create(ElementType elementType, decimal value)
    {
        ValidatorFactory.ValidateHeight(elementType, nameof(Height), value);
        return new Height(value);

    }

    public static implicit operator decimal(Height height) => height.Value;


}

