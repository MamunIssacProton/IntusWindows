using System;
using IntusWindows.Sales.Contract.Enums;
using IntusWindows.Sales.Contract.Utils;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;
using IntusWindows.Sales.Contract.DTOs;

namespace IntusWindows.Sales.Order.Web.Blazor.Hosted.Server.Hubs;

public class OrderHub: Hub
{

    [HubMethodName(nameof(SendMessage))]
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("RecieveMessage", message);
    }


    [HubMethodName(nameof(HubMethods.JoinGroup))]
    public async Task JoinGroup()
    {

        await Groups.AddToGroupAsync(Context.ConnectionId, HubGroups.Order);
    }



    [HubMethodName(nameof(HubMethods.BroadcastToGroup))]

    public async Task BroadCastToGroup(HubOperations operation, OrderDTO message)
    {

        await Clients.Group(HubGroups.Order).SendAsync("ReceiveMessage", operation, message);
        Console.WriteLine("broadcasted msg on orderhub");
    }

    [HubMethodName(nameof(LeaveGroup))]
    public async Task LeaveGroup()
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, HubGroups.Order);

    }


}

