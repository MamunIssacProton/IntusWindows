using IntusWindows.Sales.Order.Domain.Utils;

namespace IntusWindows.Sales.Order.Domain.ValueObjects;

public class StateId
{
	public Guid Value { get; private set; }

	private StateId(Guid value) => this.Value = value;

	public static StateId Create(Guid value)
	{
		ValidatorFactory.ValidateGuid(nameof(StateId),value);

		return new StateId(value);
	}

	public static implicit operator Guid (StateId stateId) => stateId.Value;

}

