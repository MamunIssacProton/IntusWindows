using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Domain.Entities;

namespace IntusWindows.Sales.Order.Infrastructure.Interfaces;

public interface IElementRepository : IBaseContextRepository
{
    ValueTask<ApiResultDTO> SaveElementAsync(Element element, string dimensionId);

    ValueTask<ApiResultDTO> DeleteElementAsync(Guid id);

    ValueTask<IReadOnlyList<ElementDTO>> GetElementsListAsync();

    ValueTask<Element?> GetElementByIdAsync(Guid id);

    ValueTask<ElementDTO?> GetElementsDTOByIdAsync(Guid id);

}

