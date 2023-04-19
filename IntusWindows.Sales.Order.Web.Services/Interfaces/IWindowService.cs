using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models.Map;

namespace IntusWindows.Sales.Order.Web.Services.Interfaces;

public interface IWindowService
{
    ValueTask<List<WindowDTO>> GetWindowListAsync();

    ValueTask<ApiResultDTO> CreateNewWindowAsync(Mapper.Window window);
}

