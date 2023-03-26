using IntusWindows.Sales.Order.Domain.Enums;
using IntusWindows.Sales.Order.Domain.Utils;

namespace IntusWindows.Sales.Order.Domain.ValueObjects;

public class DimensionId
{
    public string Value { get; private set; }


    protected DimensionId(string value) => this.Value = value;

    public static DimensionId Create(ElementType elementType, decimal width, decimal height)
    {
        ValidatorFactory.ValidateWidth(elementType, nameof(width), width);
        ValidatorFactory.ValidateHeight(elementType, nameof(height), height);

        return new DimensionId($"{elementType.ToString()}-{width} X {height}");

    }


    public static implicit operator string(DimensionId dimensionId) => dimensionId.Value;


}

