using System;
namespace IntusWindows.Sales.Order.Web.Services.Interfaces;

public interface IHubService
{
    Task SendMessage<T>(T message, string hubUrl);
    Task SubscribeToMessages<T>(Action<T> messageHandler, string hubUrl);
    void Connect(string url);
    Task JoinGroup(string groupName);
    Task LeaveGroup(string groupName);
    Task BroadCastToGroup<T>(string groupName, T message);
    Task UnsubscribeFromMessages<T>(Action<T> messageHandler, string hubUrl);
}

