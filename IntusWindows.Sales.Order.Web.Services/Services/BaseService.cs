using IntusWindows.Sales.Order.Web.Services.Interfaces;

namespace IntusWindows.Sales.Order.Web.Services.Services;

public  class BaseService : IBaseService
{
    public readonly ProgressiveHttpClient client;

    public BaseService(ProgressiveHttpClient client)
    {
        this.client = client;
        this.client.BaseAddress = new Uri(ApiEndpoints.Order);
        
    }
    
    


}

