using System;
using System.Diagnostics;

namespace IntusWindows.Sales.Order.Maui.ViewModels
{
	public class OrderViewModel:BaseViewModel,IDisposable
	{
		private readonly HubConnection? hubConnection;

		public OrderViewModel()
		{
			hubConnection = new HubConnectionBuilder().WithUrl($"{ApiEndpoints.SignalRClient}/hub/{HubGroups.Order}").Build();
			Task.Run(async () =>
			{
				await hubConnection.StartAsync();

				await hubConnection.InvokeAsync(HubMethods.JoinGroup);
			
			});
			
        }

        public void Dispose()
        {
			if (hubConnection != null)
			{
				hubConnection.DisposeAsync();
			}
        }
    }
}

