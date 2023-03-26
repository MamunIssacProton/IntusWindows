using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Api.ApplicationServices;
using IntusWindows.Sales.Order.Api.Commands;
using IntusWindows.Sales.Order.Api.Commands.Create;
using IntusWindows.Sales.Order.Api.Queries;
using Microsoft.AspNetCore.Mvc;

namespace IntusWindows.Sales.Order.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class ElementController : ControllerBase
{
    private ApplicationService applicationService { get; }

    public ElementController(ApplicationService applicationService)
    {
        this.applicationService = applicationService;
    }

    [HttpPost(Name = "create")]

    public async Task<IActionResult> Create(CreateElementCommand command)
    {
        try
        {
            await this.applicationService.HandleCommand(command);
            return Ok(command.Id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPut(Name = "/elementsById")]
    public async Task<ElementDTO?> GetElementByIdAsync(GetElementQuery query)
                                   => await this.applicationService.HandleQuery(query);




}

