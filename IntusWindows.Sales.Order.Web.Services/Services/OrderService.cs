using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models.Map;
using IntusWindows.Sales.Order.Web.Services.Interfaces;
using Newtonsoft.Json;

namespace IntusWindows.Sales.Order.Web.Services.Services;

public class OrderService:BaseService,IOrderService
{
	public OrderService(ProgressiveHttpClient client):base(client)
	{

	}

    public async ValueTask<ApiResultDTO> ChangeDimensionsFromOrderByIdAsync(List<Mapper.ChangeDimensionFormOrder> order)
    {
        var result = await client.PutAsJsonAsync<List<Mapper.ChangeDimensionFormOrder>>("/api/order/changeDimensions",order);
        return JsonConvert.DeserializeObject<ApiResultDTO>(await result.Content.ReadAsStringAsync());
    }

    public async ValueTask<ApiResultDTO> ChangeOrderNameByIdAsync(Mapper.ChangeOrderName orderName)
    {
        var result = await client.PutAsJsonAsync<Mapper.ChangeOrderName>("/api/order/changeOrderName", orderName);
        return JsonConvert.DeserializeObject<ApiResultDTO>(await result.Content.ReadAsStringAsync());
    }

    public async ValueTask<ApiResultDTO> ChangeStateInOrderByIdAsync(Mapper.ChangeStateInOrder order)
    {
        var result = await client.PutAsJsonAsync<Mapper.ChangeStateInOrder>("/api/order/changeStateInOrder", order);
        return JsonConvert.DeserializeObject<ApiResultDTO>(await result.Content.ReadAsStringAsync());
    }

    public async ValueTask<ApiResultDTO> DeleteElementsFromOrderAsync(Mapper.DeleteElementsFromOrdr order)
    {
        var result = await client.PostAsJsonAsync<Mapper.DeleteElementsFromOrdr>($"/api/order/deleteElementsFromOrder",order);

        return JsonConvert.DeserializeObject<ApiResultDTO>(await result.Content.ReadAsStringAsync());
    }

    public async Task<ApiResultDTO> DeleteOrderByIdAsync(Mapper.DeleteOrder order)
    {
        var result = await client.DeleteAsync($"/api/order/{order.OrderId}");
        return JsonConvert.DeserializeObject<ApiResultDTO>(await result.Content.ReadAsStringAsync());
    }

    public async ValueTask<ApiResultDTO> DeleteWindowFromOrderAsync(Mapper.DeleteWindowsFromOrder order)
    {
        var result = await client.PostAsJsonAsync<Mapper.DeleteWindowsFromOrder>($"/api/order/window",order);

        return JsonConvert.DeserializeObject<ApiResultDTO>(await result.Content.ReadAsStringAsync());
    }

    public async ValueTask<ApiResultDTO> GenerateNewOrder(Mapper.Order order)
    {
        var result = await client.PostAsJsonAsync<Mapper.Order>("/api/order/generate",order);

        return JsonConvert.DeserializeObject<ApiResultDTO>(await result.Content.ReadAsStringAsync());
          
    }

    public async ValueTask<List<OrderDTO>> GetOrdersListAsync()
    {
        var result = await client.GetWithProgressAsync("/api/order/list");
        return JsonConvert.DeserializeObject<List<OrderDTO>>(await result.Content.ReadAsStringAsync());
    }
}

