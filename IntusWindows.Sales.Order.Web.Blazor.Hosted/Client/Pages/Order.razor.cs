using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models;
using IntusWindows.Sales.Contract.Models.Map;
using IntusWindows.Sales.Contract.Utils;
using IntusWindows.Sales.Order.Web.Blazor.Components;
using IntusWindows.Sales.Order.Web.Blazor.ContextMenuOptions;
using IntusWindows.Sales.Order.Web.Blazor.Hosted.Client.Components;
using Microsoft.AspNetCore.Components;

namespace IntusWindows.Sales.Order.Web.Blazor.Hosted.Client.Pages
{
    public partial class Order : ComponentBase
    {
        public Order()
        {
        }

        private DialogBase dialog;

        private DialogBase dialogOrderDetails;

        private DialogBase dialogChangeState;

        private DialogBase dialogDeleteWindow;

        private DialogBase dialogDeleteConfirm;

        private DialogBase dialogDeleteElement;

        private DialogBase dialogChangeOrderName;

        private DialogBase dialogChangeDimension;

        private Mapper.Order order { get; set; } = new Mapper.Order() { Windows = new List<Guid>() };

        List<StateDTO> states { get; set; } = new List<StateDTO>();

        List<WindowDTO> windows { get; set; } = new List<WindowDTO>();

        List<WindowDTO> selectedWindows { get; set; } = new List<WindowDTO>();

        List<WindowDTO> cancelledWindows { get; set; } = new List<WindowDTO>();

        List<OrderDTO> orders { get; set; } = new List<OrderDTO>();

        List<ElementDTO> cancelledElements { get; set; } = new List<ElementDTO>();

        List<WindowTreeNode> treeNodes = new List<WindowTreeNode>();

        string selectedState { get; set; }

        bool EnableSave { get; set; } = false;

        bool EnableSaveChanges { get; set; } = false;

        bool IsChangeState { get; set; } = false;

        bool ElementSaveChangeEnabled { get; set; }

        bool ShouldExpand { get; set; } = true;

        string Message { get; set; } = string.Empty;

        string ChangedOrderName { get; set; } = string.Empty;

        List<ContextMenuItem> contextMenuItems = new List<ContextMenuItem>();

        private OrderDTO selectedOrder { get; set; }

        protected override async Task OnInitializedAsync()
        {
            states.Clear();
            windows.Clear();

            contextMenuItems.Add(new ContextMenuItem()
            {
                Text = "View Details",
                Value = "details"
            });

            contextMenuItems.Add(new ContextMenuItem()
            {
                Text = "Change Order Name",
                Value = "changeName"
            });

            contextMenuItems.Add(new ContextMenuItem()
            {
                Text = "Change State",
                Value = "changeState"
            });
            contextMenuItems.Add(new ContextMenuItem()
            {
                Text="Change Dimension from this order",
                Value= "changeDimension"
            });
            contextMenuItems.Add(new ContextMenuItem()
            {
                Text = "Delete window from this order",
                Value = "deleteWindow"
            });

            contextMenuItems.Add(new ContextMenuItem()
            {
                Text = "Delete elements from window",
                Value = "deleteElement"
            });

            contextMenuItems.Add(new ContextMenuItem()
            {
                Text = "Delete Order",
                Value = "delete"
            });



            states.AddRange(await stateService.GetStatesAsync());

            windows.AddRange(await windowsService.GetWindowListAsync());
            orders.AddRange(await orderSerive.GetOrdersListAsync());
        }

        void ToggleWindowSelection(WindowDTO window)
        {
            if (selectedWindows.Contains(window))
            {
                selectedWindows.Remove(window);

            }
            else
            {

                selectedWindows.Add(window);

            }
            EnableSave = CheckEnableSave();
            StateHasChanged();
        }
        void ToggleCancelledWindows(WindowDTO window)
        {
            if (cancelledWindows.Contains(window))
            {
                cancelledWindows.Remove(window);

            }
            else
            {
                cancelledWindows.Add(window);

            }
            EnableSaveChanges = CheckSaveChanges();
            StateHasChanged();
        }

        void ToggleCancelledElements(ElementDTO element)
        {
            if (cancelledElements.Exists(e => e.Id == element.Id))
            {
                var e = cancelledElements.First(e => e.Id == element.Id);
                cancelledElements.Remove(e);
            }
            else
            {
                cancelledElements.Add(element);
            }
            ElementSaveChangeEnabled = CheckElementRemovability();
            StateHasChanged();
        }

        void ToggleWindowExpansion(WindowDTO window)
        {
            if (selectedWindows.Contains(window))
            {
                selectedWindows.Remove(window);
                ShouldExpand = CheckEnableSave();
            }
            else
            {
                selectedWindows.Add(window);
                ShouldExpand = CheckEnableSave();
            }
        }


        bool CheckEnableSave() => selectedWindows.Count > 0 ? true : false;

        bool CheckSaveChanges() => cancelledWindows.Count > 0 ? true : false;

        bool CheckElementRemovability() => cancelledElements.Count > 0 ? true : false;
        async Task OnOrderCreate()
        {
            if (selectedWindows.Count < 1)

                return;

            Console.WriteLine("===created order====");
            Console.WriteLine($"{order.StateId}");
            Console.WriteLine("window ids");
            order.Id = Guid.NewGuid();
            foreach (var item in selectedWindows)
            {
                Console.WriteLine($"{item.Id}");

            }
            order.Windows.AddRange(selectedWindows.Select(x => x.Id));
            ValidatorFactory.Validate(order);
            var res = await orderSerive.GenerateNewOrder(order);
            Message = res.Message;

        }

        async ValueTask OnWindowsRemoveFromOrder()
        {
            Mapper.DeleteWindowsFromOrder order = new Mapper.DeleteWindowsFromOrder()
            {
                OrderId = selectedOrder.Id,
                WindowIds = new List<Guid>()
            };
            order.WindowIds = cancelledWindows.Select(x => x.Id).ToList();
            //
            Console.WriteLine($"oon delete windows dats : {order.WindowIds}");

            var response = await orderSerive.DeleteWindowFromOrderAsync(order);
            Console.WriteLine($"res:{response.Message}");
            Message = response.Ok.ToString();
        }
        public void Dispose()
        {
        }

        public void onSelectedOrder(OrderDTO order)
        {
            Console.WriteLine($"got on selected order : {order.Id}");
            selectedOrder = order;

        }

        public void SelectedContextMenu(ContextMenuItem item)
        {
            Console.WriteLine($"got menu : {item.Value}");

            switch (item.Value)
            {
                case "details":
                    dialogOrderDetails.Show();
                    StateHasChanged();
                    break;

                case "changeState":

                    dialogChangeState.Show();
                    StateHasChanged();
                    Console.WriteLine($"on change state");
                    break;

                case "deleteWindow":
                    dialogDeleteWindow.Show();
                    StateHasChanged();
                    break;

                case "deleteElement":
                    GenerateTreeNodes();
                    dialogDeleteElement.Show();
                    StateHasChanged();

                    break;

                case "delete":
                    dialogDeleteConfirm.Show();
                    StateHasChanged();
                    break;
                case "changeName":
                    dialogChangeOrderName.Show();
                    StateHasChanged();
                    break;

                case "changeDimension":
                    GenerateTreeNodes();
                    dialogChangeDimension.Show();
                    StateHasChanged();
                    break;
                default:
                    break;
            }
        }

        private async ValueTask OnChangeSateSave()
        {

            Message = selectedOrder.Id.ToString();
            var res = await orderSerive.ChangeStateInOrderByIdAsync(new Mapper.ChangeStateInOrder()
            {
                OrderId = selectedOrder.Id,
                StateId = Guid.Parse(selectedState)
            });

            Message = res.Message;
        }

        private async Task DeleteOrder()
        {
            var res = await orderSerive.DeleteOrderByIdAsync(new Mapper.DeleteOrder()
            {
                OrderId = selectedOrder.Id
            });
            Message = res.Message;
        }

        private async ValueTask OnDeleteElement()
        {
            Console.WriteLine("cancelled elements");
            foreach (var item in cancelledElements)
            {
                Console.WriteLine(item.Id);
            }

        }

        private async Task OnDimensionChanged(List<Mapper.ChangeDimensionFormOrder> dimensionsInOrder)
        {
            if (dimensionsInOrder.Count < 1)
                return;
            foreach (var order in dimensionsInOrder)
            {
                order.OrderId = selectedOrder.Id;
                
            }
            var res = await orderSerive.ChangeDimensionsFromOrderByIdAsync(dimensionsInOrder);
            if (res.Ok)
                dialogOrderDetails.CloseDialog();
        }

        void GenerateTreeNodes()
        {
            treeNodes.Clear();
            foreach (var window in selectedOrder.windows)
            {
                var node = new WindowTreeNode()
                {
                    WindowId = window.Id,
                    WindowName = window.windowName,
                    elements = new List<ElementNode>(),
                    IsExpanded = false,

                };
                foreach (var element in window.Elements)
                {
                    node.elements.Add(new ElementNode(window.Id,element.Id.Value,element.elementName,false,element.dimensionId));
                }
               // node.elements.AddRange(window.);


                treeNodes.Add(node);

            }
        }

            async Task OnChangeOrderName()
            {
                if (string.IsNullOrEmpty(ChangedOrderName))
                    return;
                OrderDTO modifiedName = this.selectedOrder with { OrderName = ChangedOrderName };
                var res = await orderSerive.ChangeOrderNameByIdAsync(new Mapper.ChangeOrderName()
                {
                    Id = modifiedName.Id,
                    OrderName = modifiedName.OrderName
                });
                Console.WriteLine($"res : {res.Message}");

            }

            async Task OnConfirmElementsDeletion(List<ElementNode> elements)
            {
                if (selectedOrder != null)
                {
                    var response = await orderSerive.DeleteElementsFromOrderAsync(
                         new Mapper.DeleteElementsFromOrdr()
                         {
                             OrderId = selectedOrder.Id,
                             Elements = elements
                         });

                    Console.WriteLine($"res: {response.Message}");
                }
            }
        }
    
}

