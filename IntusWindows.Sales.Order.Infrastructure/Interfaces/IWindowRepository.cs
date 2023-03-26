using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Domain.Entities;
namespace IntusWindows.Sales.Order.Infrastructure.Interfaces;

public interface IWindowRepository : IBaseContextRepository
{
    Task<ApiResultDTO> AddWindow(Window window);

    Task<IReadOnlyList<Window>> GetAllWindowsListAsync();

    Task<Window> GetWindowByIdAsync(Guid id);

}

