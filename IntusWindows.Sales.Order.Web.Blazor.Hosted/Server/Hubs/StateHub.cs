using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Enums;
using IntusWindows.Sales.Contract.Utils;
using Microsoft.AspNetCore.SignalR;

namespace IntusWindows.Sales.Order.Web.Blazor.Hosted.Server.Hubs;

public class StateHub:Hub
{
    [HubMethodName(nameof(SendMessage))]
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("RecieveMessage", message);
    }

    [HubMethodName(nameof(HubMethods.SendNotification))]
    public async Task SendNotification( string message)
    {
        await Clients.User(Context.ConnectionId).SendAsync(HubMethods.ReceiveMessage, message);
    }

    [HubMethodName(nameof(HubMethods.JoinGroup))]
    public async Task JoinGroup()
    {

        await Groups.AddToGroupAsync(Context.ConnectionId, HubGroups.State);
    }



    [HubMethodName(nameof(HubMethods.BroadcastToGroup))]

    public async Task BroadCastToGroup(HubOperations operation, StateDTO message)
    {

        await Clients.Group(HubGroups.State).SendAsync("ReceiveMessage", operation, message);
    }

    [HubMethodName(nameof(LeaveGroup))]
    public async Task LeaveGroup()
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, HubGroups.State);

    }
}

