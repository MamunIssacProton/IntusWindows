using System;
using IntusWindows.Sales.Contract.Models.Map;
using IntusWindows.Sales.Order.Web.Services.Interfaces;
using Newtonsoft.Json;

namespace IntusWindows.Sales.Order.Web.Services.Services;

public class StateService:BaseService,IStateService
{
	public StateService(ProgressiveHttpClient client):base(client)
	{
	}

	public async Task<IReadOnlyList<Mapper.State>> GetStatesAsync()
	{
		var response = await client.GetWithProgressAsync("/api/State/list");
		if (response.IsSuccessStatusCode)
			return JsonConvert.DeserializeObject<IReadOnlyList<Mapper.State>>(await response.Content.ReadAsStringAsync());
		return new List<Mapper.State>().AsReadOnly();
	}
}

