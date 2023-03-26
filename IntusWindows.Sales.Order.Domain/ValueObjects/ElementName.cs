using System;
using IntusWindows.Sales.Order.Domain.Utils;

namespace IntusWindows.Sales.Order.Domain.ValueObjects;

public class ElementName
{
    public string Value { get; private set; }

    protected ElementName(string value) => this.Value = value;

    public static ElementName Create(string value)
    {
        ValidatorFactory.ValidateName(nameof(ElementName), value);

        return new ElementName(value);

    }
    public static implicit operator string(ElementName name) => name.Value;

}

