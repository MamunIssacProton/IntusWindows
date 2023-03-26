using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Domain.Entities;

namespace IntusWindows.Sales.Order.Infrastructure.Interfaces;

public interface IDimensionRepository : IBaseRepository
{
    public Task<IReadOnlyList<Dimension>> GetAllDimensionsListAsync();

    public Task<ApiResultDTO> SaveDimensionAsync(Dimension dimension);

    public Task<Dimension?> GetDimensionByIdAsync(string id);

    public Task<ApiResultDTO> UpdateDimensionAsync(string id, decimal height, decimal width, string title);

}

