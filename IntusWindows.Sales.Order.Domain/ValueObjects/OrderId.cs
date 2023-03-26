using IntusWindows.Sales.Order.Domain.Utils;

namespace IntusWindows.Sales.Order.Domain.ValueObjects;

public class OrderId
{
	public Guid Value { get; private set; }

	private OrderId(Guid value) => this.Value = value;

	public static OrderId Create(Guid value)
	{
		ValidatorFactory.ValidateGuid(nameof(OrderId), value);

		return new OrderId(value);
	}

	public static implicit operator Guid(OrderId orderId) => orderId.Value;


}

