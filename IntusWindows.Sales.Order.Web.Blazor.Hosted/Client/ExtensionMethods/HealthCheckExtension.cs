using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace IntusWindows.Sales.Order.Web.Blazor.Hosted.Client.ExtensionMethods;

public static class HealthCheckExtension
{
	public static  void AddExternalHealthCheck(this IServiceCollection services)
	{
	    services.AddHealthChecks().AddAsyncCheck("api", async ()=>
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"/api/health");

            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy();
            }
            else
            {
                return HealthCheckResult.Unhealthy($"External API is not healthy. Status code: {response.StatusCode}");
            }
        });

	
	}
}

