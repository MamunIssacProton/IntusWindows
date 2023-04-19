using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Enums;
using IntusWindows.Sales.Contract.Utils;
using Microsoft.AspNetCore.SignalR;

namespace IntusWindows.Sales.Order.Web.Blazor.Hosted.Server.Hubs;

public class DimensionHub:Hub
{
    [HubMethodName(nameof(SendMessage))]
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("RecieveMessage", message);
    }


    [HubMethodName(nameof(HubMethods.JoinGroup))]
    public async Task JoinGroup()
    {

        await Groups.AddToGroupAsync(Context.ConnectionId, HubGroups.Dimension);
    }


    [HubMethodName(nameof(HubMethods.BroadcastToGroup))]

    public async Task BroadCastToGroup(HubOperations operation,DimensionDTO message)
    {

        await Clients.Group(HubGroups.Dimension).SendAsync("ReceiveMessage",operation ,message);
    }

    [HubMethodName(nameof(LeaveGroup))]
    public async Task LeaveGroup()
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, HubGroups.Dimension);

    }
}

