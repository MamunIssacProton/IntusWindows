using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models.Map;

namespace IntusWindows.Sales.Order.Web.Services.Interfaces;

public interface IStateService
{
    ValueTask<IReadOnlyList<StateDTO>> GetStatesAsync();

    ValueTask<ApiResultDTO> CreateState(Mapper.State state);
}

