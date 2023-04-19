using System;
using IntusWindows.Sales.Order.Web.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace IntusWindows.Sales.Order.Web.Services.Services;

public class SignalrService
{
    private readonly HubConnection connection;
    readonly string baseHub = "https://localhost:7042/";

    public SignalrService(string hubUri)
	{
        connection = new HubConnectionBuilder()
                      .WithUrl($"{baseHub}/{hubUri}")
                      .WithAutomaticReconnect()
                      .Build();
	}

    public Task BroadCastToGroup<T>(string groupName, T message)
    {
        throw new NotImplementedException();
    }

    public Task JoinGroup(string groupName)
    {
        throw new NotImplementedException();
    }

    public Task LeaveGroup(string groupName)
    {
        throw new NotImplementedException();
    }

    public async Task SendMessage<T>(T message, string groupName,string hubUrl = "/order")
    {
        await connection.InvokeAsync("BroadCastToGroup",groupName, message);
    }

    public async Task SendMessage<T>(T message, string hubUrl = "/order")
    {
        await connection.InvokeAsync("SendMessage", message);
    }

    public async Task SubscribeToMessages<T>(Action<T> messageHandler, string hubUrl = "/order")
    {
        await connection.StartAsync();
        connection.On<T>("ReceiveMessage", message =>
        {
            messageHandler(message);
        });
    }

    public void UnsubscribeFromMessages<T>(Action<T> messageHandler, string hubUrl = "/order")
    {
        connection.Remove("RecieveMessage");
        connection.StopAsync().Wait();
        Console.WriteLine("Unsubscribed");
    }
}

