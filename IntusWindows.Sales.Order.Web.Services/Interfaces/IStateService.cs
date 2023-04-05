using System;
using IntusWindows.Sales.Contract.Models.Map;

namespace IntusWindows.Sales.Order.Web.Services.Interfaces;

public interface IStateService
{
     Task<IReadOnlyList<Mapper.State>> GetStatesAsync();
}

