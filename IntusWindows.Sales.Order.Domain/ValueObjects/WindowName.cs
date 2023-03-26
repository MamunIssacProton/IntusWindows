using IntusWindows.Sales.Order.Domain.Utils;

namespace IntusWindows.Sales.Order.Domain.ValueObjects;

public class WindowName
{
    public string? Value { get; private set; }

    protected WindowName(string? value) => this.Value = value;

    public static WindowName Create(string? value)
    {
        ValidatorFactory.ValidateName(nameof(WindowName), value);

        return new WindowName(value);
    }

    public static implicit operator string?(WindowName windowName) => windowName.Value;



}

