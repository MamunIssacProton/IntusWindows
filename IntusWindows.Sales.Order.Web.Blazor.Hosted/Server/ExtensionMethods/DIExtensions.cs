using System;
using IntusWindows.Sales.Contract.Utils;
using IntusWindows.Sales.Order.Web.Blazor.Hosted.Server.Hubs;

namespace IntusWindows.Sales.Order.Web.Blazor.Hosted.Server.ExtensionMethods;

public static class DIExtensions
{
	public static IApplicationBuilder MapAllHubs(this IApplicationBuilder app)
	{
		app.UseEndpoints(endpoint=>
		{
			endpoint.MapHub<StateHub>($"/hub/{HubGroups.State}");
            endpoint.MapHub<WindowHub>($"/hub/{HubGroups.Window}");
			endpoint.MapHub<DimensionHub>($"/hub/{HubGroups.Dimension}");
			endpoint.MapHub<ElementHub>($"/hub/{HubGroups.Element}");
			endpoint.MapHub<OrderHub>($"/hub/{HubGroups.Order}");

        });

		return app;
	}
}

