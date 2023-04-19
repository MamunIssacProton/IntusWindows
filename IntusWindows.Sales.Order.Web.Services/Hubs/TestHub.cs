using System;
using IntusWindows.Sales.Order.Web.Services.Interfaces;
using IntusWindows.Sales.Order.Web.Services.Services;
using Microsoft.AspNetCore.SignalR;
namespace IntusWindows.Sales.Order.Web.Services.Hubs;

public class TestHub:BaseHubService, IHubService

{
   public TestHub(string uri) : base(uri)
    {
    }

   
}

