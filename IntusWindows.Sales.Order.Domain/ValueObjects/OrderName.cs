using System;
using IntusWindows.Sales.Order.Domain.Utils;

namespace IntusWindows.Sales.Order.Domain.ValueObjects;

public class OrderName
{
	public  string Value { get; private set; }

	private OrderName(string value) => this.Value = value;

	public static OrderName Create(string value)
	{
		ValidatorFactory.ValidateName(nameof(OrderName), value);
		return new OrderName(value);
	}

	public static implicit operator string(OrderName orderName) => orderName.Value;

}

