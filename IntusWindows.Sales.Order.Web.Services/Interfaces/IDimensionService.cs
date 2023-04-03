using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models;

namespace IntusWindows.Sales.Order.Web.Services.Interfaces;

public interface IDimensionService:IBaseService
{
    public ValueTask<IReadOnlyList<DimensionDTO>> GetAllDimensionsListAsync(IProgress<int>progress=null);

    public ValueTask<ApiResultDTO> SaveDimensionAsync(Dimension dimension);

   // public ValueTask<DimensionDTO?> GetDimensionByIdAsync(string id);

    public ValueTask<ApiResultDTO> UpdateDimensionAsync(UpdateDimension dimension);

    public event Action<int> ProgressChanged; 
}

