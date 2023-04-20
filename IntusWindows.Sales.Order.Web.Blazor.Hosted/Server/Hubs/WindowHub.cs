using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Enums;
using IntusWindows.Sales.Contract.Utils;
using Microsoft.AspNetCore.SignalR;

namespace IntusWindows.Sales.Order.Web.Blazor.Hosted.Server.Hubs;

public class WindowHub:Hub
{
    [HubMethodName(nameof(SendMessage))]
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("RecieveMessage", message);
    }

    [HubMethodName(nameof(HubMethods.SendNotification))]
    public async Task SendNotification(string connectionId, string message)
    {
        await Clients.User(connectionId).SendAsync(HubMethods.ReceiveMessage, message);
    }

    [HubMethodName(nameof(HubMethods.JoinGroup))]
    public async Task JoinGroup()
    {

        await Groups.AddToGroupAsync(Context.ConnectionId, HubGroups.Window);
    }


    [HubMethodName(nameof(HubMethods.BroadcastToGroup))]

    public async Task BroadCastToGroup(HubOperations operation,WindowDTO message)
    {

        await Clients.Group(HubGroups.Window).SendAsync("ReceiveMessage", operation , message);
    }

    [HubMethodName(nameof(LeaveGroup))]
    public async Task LeaveGroup()
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, HubGroups.Window);

    }
}

