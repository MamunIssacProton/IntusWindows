using System.Net.Http.Json;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models.Map;
using IntusWindows.Sales.Order.Web.Services.Interfaces;
using Newtonsoft.Json;

namespace IntusWindows.Sales.Order.Web.Services.Services;

public class ElementService : BaseService, IElementService
{
    public ElementService(ProgressiveHttpClient client) : base(client)
    {
    }

    public async ValueTask<ApiResultDTO> CreateElement(Mapper.Element element)
    {

        var response = await client.PostAsJsonAsync<Mapper.Element>("api/Element/Create", element);

        return JsonConvert.DeserializeObject<ApiResultDTO>(await response.Content.ReadAsStringAsync());
    }

    public async ValueTask<IReadOnlyList<ElementDTO>> GetElementsAsync()
    {
        var response = await client.GetWithProgressAsync("/api/Element/list");
        if (response.IsSuccessStatusCode)
            return JsonConvert.DeserializeObject<IReadOnlyList<ElementDTO>>(await response.Content.ReadAsStringAsync());
        return new List<ElementDTO>().AsReadOnly();
    }
}

