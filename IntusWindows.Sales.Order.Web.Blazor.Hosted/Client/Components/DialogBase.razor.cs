using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components;

namespace IntusWindows.Sales.Order.Web.Blazor.Hosted.Client.Components;

public partial class DialogBase:ComponentBase
{
   

    private bool _showDialog;

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public EventCallback OnSave { get; set; }

    [Parameter]
    public string CreateButtonText { get; set; } = "Create";

    [Parameter]
    public string CancelButtonText { get; set; } = "Cancel";

    [Parameter]
    public bool IsSave { get; set; }


    private async Task HandleSave()
    {
        Console.WriteLine($"invoked on saved");
        await OnSave.InvokeAsync();

        CloseDialog();
    }

    public void Show()
    {
        _showDialog = true;
        StateHasChanged();
    }

    public void CloseDialog()
    {
        _showDialog = false;
        StateHasChanged();
    }
}

