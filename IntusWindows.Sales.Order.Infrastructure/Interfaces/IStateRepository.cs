using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Domain.Entities;

namespace IntusWindows.Sales.Order.Infrastructure.Interfaces;

public interface IStateRepository: IBaseContextRepository
{
	Task<ApiResultDTO> CreateNewStateAsync(State state);

	Task<IReadOnlyList<State>> GetAllStatesListAsync();

	Task<State> GetStateByIdAsync(Guid id);

	Task<ApiResultDTO> ChangeStateNameAsync(State state);


}

