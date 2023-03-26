using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Domain.Entities;

namespace IntusWindows.Sales.Order.Infrastructure.Interfaces;

public interface IElementRepository : IBaseContextRepository
{
    Task<ApiResultDTO> SaveElementAsync(Element element, string dimensionId);

    Task<ApiResultDTO> DeleteElementAsync(Guid id);

    Task<IReadOnlyList<Element>> GetElementsListAsync();

    Task<Element?> GetElementByIdAsync(Guid id);

    Task<ElementDTO?> GetElementsDTOByIdAsync(Guid id);

}

