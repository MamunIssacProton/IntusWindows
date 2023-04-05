using System;
using System.Net.Http.Json;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models.Map;
using IntusWindows.Sales.Order.Web.Services.Interfaces;
using Newtonsoft.Json;

namespace IntusWindows.Sales.Order.Web.Services.Services;

public class StateService:BaseService,IStateService
{
	public StateService(ProgressiveHttpClient client):base(client)
	{
	}

    public async ValueTask<ApiResultDTO> CreateState(Mapper.State state)
    {
		var response = await client.PostAsJsonAsync<Mapper.State>("/api/State/create", state);
		return JsonConvert.DeserializeObject<ApiResultDTO>(await response.Content.ReadAsStringAsync());
    }

    public async ValueTask<IReadOnlyList<StateDTO>> GetStatesAsync()
	{
		var response = await client.GetWithProgressAsync("/api/State/list");
		if (response.IsSuccessStatusCode)
			return JsonConvert.DeserializeObject<IReadOnlyList<StateDTO>>(await response.Content.ReadAsStringAsync());
		return new List<StateDTO>().AsReadOnly();
	}
}

