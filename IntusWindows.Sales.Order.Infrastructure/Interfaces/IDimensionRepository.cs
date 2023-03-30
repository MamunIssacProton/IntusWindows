using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Domain.Entities;

namespace IntusWindows.Sales.Order.Infrastructure.Interfaces;

public interface IDimensionRepository : IBaseRepository
{
    public ValueTask<IReadOnlyList<DimensionDTO>> GetAllDimensionsListAsync();

    public ValueTask<ApiResultDTO> SaveDimensionAsync(Dimension dimension);

    public ValueTask<Dimension?> GetDimensionByIdAsync(string id);

    public ValueTask<ApiResultDTO> UpdateDimensionAsync(string id, decimal height, decimal width, string title);

}

