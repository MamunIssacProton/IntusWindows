using System.Net.Http.Json;
using System.Text;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models;
using IntusWindows.Sales.Order.Web.Services.Interfaces;
using Newtonsoft.Json;
namespace IntusWindows.Sales.Order.Web.Services.Services;

public  class DimensionService:BaseService,IDimensionService
{


    public DimensionService(ProgressiveHttpClient client) : base(client)
    {
    }

    public event Action<int> ProgressChanged;

    public async ValueTask<IReadOnlyList<DimensionDTO>> GetAllDimensionsListAsync()
    {
        //var response = await client.GetAsync("/api/Dimension/list",HttpCompletionOption.ResponseContentRead);

        //var contentLength = response.Content.Headers.ContentLength;
        //var buffer = new byte[4096];
        //var stream = await response.Content.ReadAsStreamAsync();
        //var bytesRead = 0;
        //var totalBytesRead = 0;

        //while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        //{
        //    totalBytesRead += bytesRead;
        //    if (contentLength.HasValue && progress != null)
        //    {
        //        var percentage = (int)Math.Round((double)totalBytesRead / contentLength.Value * 100);
        //        progress.Report(percentage);
        //        ProgressChanged.Invoke(percentage);

        //    }
        //}

        var response = await client.GetWithProgressAsync("/api/Dimension/list");
       return JsonConvert.DeserializeObject<IReadOnlyList<DimensionDTO>>(await response.Content.ReadAsStringAsync());
    }

    //public ValueTask<DimensionDTO?> GetDimensionByIdAsync(string id)
    //{
    //    throw new NotImplementedException();
    //}

    public async ValueTask<ApiResultDTO> SaveDimensionAsync(Dimension dimension)
    {
        var response = await client.PostAsJsonAsync<Dimension>("/api/Dimension/create", dimension);
       
        return JsonConvert.DeserializeObject<ApiResultDTO>(await response.Content.ReadAsStringAsync());
    }

    public async ValueTask<ApiResultDTO> UpdateDimensionAsync(UpdateDimension dimension)
    {
        var response = await client.PutAsJsonAsync<UpdateDimension>("/api/Dimension/update", dimension);
        return JsonConvert.DeserializeObject<ApiResultDTO>(await response.Content.ReadAsStringAsync());
       
    }
}

