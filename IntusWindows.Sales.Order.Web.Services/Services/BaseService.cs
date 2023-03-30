using IntusWindows.Sales.Order.Web.Services.Interfaces;

namespace IntusWindows.Sales.Order.Web.Services.Services;

public  class BaseService : IBaseService
{
   public readonly HttpClient client;

    public BaseService(HttpClient client)
    {
        this.client = client;
        this.client.BaseAddress = new Uri(ApiEndpoints.Order);

    }
    



}

