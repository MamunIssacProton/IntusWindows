using System.Text;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models;
using IntusWindows.Sales.Order.Web.Services.Interfaces;
using Newtonsoft.Json;
namespace IntusWindows.Sales.Order.Web.Services.Services;

public  class DimensionService:BaseService,IDimensionService
{
    public DimensionService(HttpClient client) : base(client)
    {
    }
   

    public async ValueTask<IReadOnlyList<DimensionDTO>> GetAllDimensionsListAsync()
    {
        var response = await client.GetAsync("/dimensions");
        if (response.IsSuccessStatusCode)
        {
            return  JsonConvert.DeserializeObject<IReadOnlyList<DimensionDTO>>(await response.Content.ReadAsStringAsync());
        }
        return new List<DimensionDTO>();
    }

    public ValueTask<DimensionDTO?> GetDimensionByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<ApiResultDTO> SaveDimensionAsync(Dimension dimension)
    {
      var response= await client.PostAsync("dimensions/create", new StringContent(JsonConvert.SerializeObject(dimension),Encoding.UTF8));
     // response.EnsureSuccessStatusCode();
       return JsonConvert.DeserializeObject<ApiResultDTO>(await response.Content.ReadAsStringAsync());
    }

    public ValueTask<ApiResultDTO> UpdateDimensionAsync(string id, decimal height, decimal width, string title)
    {
        throw new NotImplementedException();
    }
}

