using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models.Map;

namespace IntusWindows.Sales.Order.Web.Services.Interfaces;

public interface IElementService
{
    ValueTask<ApiResultDTO> CreateElement(Mapper.Element element);

    ValueTask<IReadOnlyList<ElementDTO>> GetElementsAsync();

   // ValueTask<IReadOnlyList>
}

