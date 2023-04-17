using System;
using IntusWindows.Sales.Order.Web.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace IntusWindows.Sales.Order.Web.Services.Services;

public class BaseHubService:Hub,IHubService
{
    private readonly HubConnection connection;
    private readonly string baseUri=ApiEndpoints.SignalRClient;
    public BaseHubService(string hubUri)
	{
        connection = new HubConnectionBuilder()
                        .WithUrl($"{baseUri}/{hubUri}")
                        .WithAutomaticReconnect()
                        .Build();
	}

    [HubMethodName("BroadCastToGroup")]
    public async Task BroadCastToGroup<T>(string groupName, T message)
    {
        await Clients.Group(groupName).SendAsync("RecieveMessage", message);
    }

    [HubMethodName("JoinGroup")]
    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    [HubMethodName(nameof(LeaveGroup))]
    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }

    //[HubMethodName(nameof(SendMessage))]
    //public async Task SendMessage(string message)
    //{
    //    await Clients.All.SendAsync("ReceiveMessage", message);
    //}
    [HubMethodName(nameof(SendMessage))]
    public async Task SendMessage<T>(T message, string hubUrl = "order")
    {
        await Clients.All.SendAsync("RecieveMessage", hubUrl, message);
    }

    [HubMethodName(nameof(SubscribeToMessages))]
    public async Task SubscribeToMessages<T>(Action<T> messageHandler, string hubUrl = "order")
    {
        await connection.StartAsync();
        connection.On<T>("ReceiveMessage", message =>
        {
            messageHandler(message);
        });
    }

    [HubMethodName(nameof(UnsubscribeFromMessages))]
    public async Task UnsubscribeFromMessages<T>(Action<T> messageHandler, string hubUrl = "/order")
    {
        connection.Remove("ReceiveMessage");
       await connection.StopAsync();
    }
}

