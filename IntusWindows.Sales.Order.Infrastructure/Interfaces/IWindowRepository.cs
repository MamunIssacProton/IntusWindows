using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Domain.Entities;
namespace IntusWindows.Sales.Order.Infrastructure.Interfaces;

public interface IWindowRepository : IBaseContextRepository
{
    ValueTask<ApiResultDTO> AddWindow(Window window);

    ValueTask<IReadOnlyList<Window>> GetAllWindowsListAsync();

    ValueTask<Window> GetWindowByIdAsync(Guid id);

}

