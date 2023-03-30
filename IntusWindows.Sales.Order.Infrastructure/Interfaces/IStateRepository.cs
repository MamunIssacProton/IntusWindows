using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Domain.Entities;

namespace IntusWindows.Sales.Order.Infrastructure.Interfaces;

public interface IStateRepository: IBaseContextRepository
{
	ValueTask<ApiResultDTO> CreateNewStateAsync(State state);

	ValueTask<IReadOnlyList<State>> GetAllStatesListAsync();

	ValueTask<State> GetStateByIdAsync(Guid id);

	ValueTask<ApiResultDTO> ChangeStateNameAsync(State state);


}

