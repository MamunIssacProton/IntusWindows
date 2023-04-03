using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models.Map;

namespace IntusWindows.Sales.Order.Web.Services.Interfaces;

public interface IElementService
{
    Task<ApiResultDTO> CreateElement(Mapper.Element element);
}

