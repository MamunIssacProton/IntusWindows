using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models;
using IntusWindows.Sales.Contract.Models.Map;

namespace IntusWindows.Sales.Order.Web.Services.Interfaces;

public interface IDimensionService:IBaseService
{
    public ValueTask<IReadOnlyList<DimensionDTO>> GetAllDimensionsListAsync();

    public ValueTask<ApiResultDTO> SaveDimensionAsync(Mapper.Dimension dimension);

   // public ValueTask<DimensionDTO?> GetDimensionByIdAsync(string id);

    public ValueTask<ApiResultDTO> UpdateDimensionAsync(Mapper.UpdateDimension dimension);

   
}

