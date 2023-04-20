using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Enums;
using IntusWindows.Sales.Contract.Utils;
using Microsoft.AspNetCore.SignalR;

namespace IntusWindows.Sales.Order.Web.Blazor.Hosted.Server.Hubs;

public class HealthHub:Hub
{
    [HubMethodName(nameof(HubMethods.JoinGroup))]
    public async Task JoinGroup()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, HubGroups.DiagnosticCenter);
        Console.WriteLine($"{Context.ConnectionId} has joined diagnostic");
    }



    [HubMethodName(nameof(HubMethods.BroadcastToGroup))]

    public async Task BroadCastToGroup(string message)
    {

        await Clients.Group(HubGroups.DiagnosticCenter).SendAsync("ReceiveMessage", message);
        Console.WriteLine("boradcasted msg");
       
    }

    [HubMethodName(nameof(LeaveGroup))]
    public async Task LeaveGroup()
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, HubGroups.DiagnosticCenter);

    }
}

