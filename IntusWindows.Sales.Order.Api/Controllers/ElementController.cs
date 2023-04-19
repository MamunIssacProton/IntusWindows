using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Api.ApplicationServices;
using IntusWindows.Sales.Order.Api.Commands;
using IntusWindows.Sales.Order.Api.Commands.Create;
using IntusWindows.Sales.Order.Api.Queries;
using Microsoft.AspNetCore.Mvc;

namespace IntusWindows.Sales.Order.Api.Controllers;

[Route("api/[controller]"), ApiController]

public class ElementController : ControllerBase
{
    private ApplicationService applicationService { get; }

    public ElementController(ApplicationService applicationService)
    {
        this.applicationService = applicationService;
    }
    [HttpGet("list")]
    public async ValueTask<IReadOnlyList<ElementDTO>> List()
                                                      =>await this.applicationService.GetAllElementListAsync();

    [HttpPost("create")]

    public async ValueTask<IActionResult> Create(CreateElementCommand command)
    {
        try
        {
          
            return Ok(await this.applicationService.HandleCommand(command));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("elementById/{Id}/{WindowId}/{ElementId}")]
    public async ValueTask<ElementDTO?> GetElementByIdAsync(GetElementQuery query)
                                   => await this.applicationService.HandleQuery(query);




}

