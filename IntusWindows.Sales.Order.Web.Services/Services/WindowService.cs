using System.Net.Http.Json;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models.Map;
using IntusWindows.Sales.Order.Web.Services.Interfaces;
using Newtonsoft.Json;

namespace IntusWindows.Sales.Order.Web.Services.Services;

public class WindowService:BaseService, IWindowService
{
	public WindowService(ProgressiveHttpClient client):base(client)
	{

	}

    public async ValueTask<ApiResultDTO> CreateNewWindowAsync(Mapper.Window window)
    {
        var response = await client.PostAsJsonAsync<Mapper.Window>("/api/Window/create", window);

        return JsonConvert.DeserializeObject<ApiResultDTO>(await response.Content.ReadAsStringAsync());
    }

    public async ValueTask<List<WindowDTO>> GetWindowListAsync()
    {
        var response = await client.GetWithProgressAsync("/api/Window/list");
        return JsonConvert.DeserializeObject<List<WindowDTO>>(await response.Content.ReadAsStringAsync()); 
    }
}

