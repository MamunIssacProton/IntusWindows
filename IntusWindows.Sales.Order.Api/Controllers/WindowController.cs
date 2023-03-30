using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Api.ApplicationServices;
using IntusWindows.Sales.Order.Api.Commands;
using IntusWindows.Sales.Order.Api.Commands.Create;
using IntusWindows.Sales.Order.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IntusWindows.Sales.Order.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WindowController : ControllerBase
{
    readonly ApplicationService applicationService;
    public WindowController(ApplicationService service)
    {
        applicationService = service;
    }
    [HttpPost("create")]
    public async ValueTask<ApiResultDTO> Create(CreateWindowCommand command)
    {
        try
        {
            return await applicationService.HandleCommand(command);
        }
        catch (Exception ex)
        {
            return new ApiResultDTO(false, ex.Message);
        }
    }

    [HttpGet("/getWindowsList")]
    public async ValueTask<IReadOnlyList<Window>> GetWindowsAsync() => await this.applicationService.GetAllWindowQuery();
}

