using IntusWindows.Sales.Order.Domain.Utils;

namespace IntusWindows.Sales.Order.Domain.ValueObjects;

public class DimensionTitle
{
    public string Value { get; private set; }

    protected DimensionTitle(string value) => this.Value = value;

    public static DimensionTitle Create(string value)
    {
        ValidatorFactory.ValidateString(nameof(DimensionTitle), value);

        return new DimensionTitle(value);

    }

    public static implicit operator string(DimensionTitle title) => title.Value;
}

