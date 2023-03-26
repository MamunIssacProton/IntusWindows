using IntusWindows.Sales.Order.Domain.Utils;

namespace IntusWindows.Sales.Order.Domain.ValueObjects;

public class StateName
{
	public string Value { get; private set; }

	private StateName(string value) => this.Value = value;

	public static StateName Create(string value)
	{
		ValidatorFactory.ValidateName(nameof(StateName),value);
		return new StateName(value);

	}

	public static implicit operator string(StateName stateName) => stateName.Value;

}

