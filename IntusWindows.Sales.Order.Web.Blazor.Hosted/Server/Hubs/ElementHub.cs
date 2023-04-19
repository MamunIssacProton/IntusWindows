using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Enums;
using IntusWindows.Sales.Contract.Utils;
using Microsoft.AspNetCore.SignalR;

namespace IntusWindows.Sales.Order.Web.Blazor.Hosted.Server.Hubs;

public class ElementHub:Hub
{
    [HubMethodName(nameof(SendMessage))]
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("RecieveMessage", message);
    }


    [HubMethodName(nameof(HubMethods.JoinGroup))]
    public async Task JoinGroup()
    {

        await Groups.AddToGroupAsync(Context.ConnectionId, HubGroups.Element);
    }



    [HubMethodName(nameof(HubMethods.BroadcastToGroup))]

    public async Task BroadCastToGroup(HubOperations operation, ElementDTO message)
    {

        await Clients.Group(HubGroups.Element).SendAsync("ReceiveMessage", operation, message);
    }

    [HubMethodName(nameof(LeaveGroup))]
    public async Task LeaveGroup()
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, HubGroups.Element);

    }
}

