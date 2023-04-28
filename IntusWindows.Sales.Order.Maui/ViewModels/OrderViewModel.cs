using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace IntusWindows.Sales.Order.Maui.ViewModels
{
    public class OrderViewModel : BaseViewModel, IDisposable
    {
        private readonly HubConnection? hubConnection;

        readonly IStateService stateService;

        readonly IWindowService windowService;

        readonly IOrderService orderService;

     
        public ObservableCollection<StateDTO> states { get; private set; }

        public ObservableCollection<WindowDTO> windows { get; private set; }

        public ObservableCollection<WindowDTO> selectedWindows { get; private set; }

        public ObservableCollection<OrderDTO> orders { get; private set; }

        public ObservableCollection<ElementDTO> cancelledElements { get; private set; }

        public ObservableCollection<WindowTreeNode> treeNodes { get; private set; }

        private OrderDTO selectedOrder { get; set; }

        public OrderViewModel()
        {
            hubConnection = new HubConnectionBuilder().WithUrl($"{ApiEndpoints.SignalRClient}/hub/{HubGroups.Order}").Build();
            Task.Run(async () =>
            {
                await hubConnection.StartAsync();

                await hubConnection.InvokeAsync(HubMethods.JoinGroup);

            });

            states = new ObservableCollection<StateDTO>();
            windows = new ObservableCollection<WindowDTO>();
            selectedWindows = new ObservableCollection<WindowDTO>();
            orders = new ObservableCollection<OrderDTO>();
            cancelledElements = new ObservableCollection<ElementDTO>();
            treeNodes = new ObservableCollection<WindowTreeNode>();

            states.Clear();
            windows.Clear();
            selectedWindows.Clear();
            orders.Clear();
            cancelledElements.Clear();
            treeNodes.Clear();

            var serviceProvider = DependencyService.Resolve<IServiceProvider>();
            orderService = serviceProvider.GetService<IOrderService>();
            windowService = serviceProvider.GetService<IWindowService>();
            stateService = serviceProvider.GetService<IStateService>();

            Task.Run(async () =>
            {
                IsRunning = true;
                foreach (var order in await orderService.GetOrdersListAsync())
                {
                    orders.Add(order);
                   
                }

                foreach (var state in await stateService.GetStatesAsync())
                {

                    states.Add(state);
                }

                foreach (var window in await windowService.GetWindowListAsync())
                {
                    windows.Add(window);
                }

                IsRunning = false;
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

