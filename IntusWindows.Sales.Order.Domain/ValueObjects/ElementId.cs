using System.ComponentModel.DataAnnotations;
using IntusWindows.Sales.Order.Domain.Utils;

namespace IntusWindows.Sales.Order.Domain.ValueObjects;

public class ElementId
{
    public Guid Value { get; private set; }

    protected ElementId(Guid value) => this.Value = value;

    public static ElementId Create(Guid value)
    {
        ValidatorFactory.ValidateGuid(nameof(ElementId), value);
        return new ElementId(value);
    }

    public static implicit operator Guid(ElementId elementId) => elementId.Value;
}

