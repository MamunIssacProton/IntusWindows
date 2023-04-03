using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Api.ApplicationServices;
using IntusWindows.Sales.Order.Api.Commands;
using IntusWindows.Sales.Order.Api.Commands.Create;
using IntusWindows.Sales.Order.Api.Commands.Update;
using IntusWindows.Sales.Order.Domain.Entities;
using IntusWindows.Sales.Order.Domain.Enums;
using IntusWindows.Sales.Order.Domain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace IntusWindows.Sales.Order.Api.Controllers;

[Route("api/[controller]"),ApiController]

public class DimensionController : ControllerBase
{
    readonly ApplicationService applicationService;
    public DimensionController(ApplicationService service)
    {
        applicationService = service;

    }
    
    [HttpGet("list")]
    public async ValueTask<IReadOnlyList<DimensionDTO>> GetDimensionsAsync()
                                                  => await this.applicationService.GetDimensionsAsync();



    [HttpPost("create")]
    public async ValueTask<ApiResultDTO> Create(CreateDimensionCommand command)
    {

        try
        {

            ValidatorFactory.ValidateElementType(command.elementType);
            ValidatorFactory.ValidateName(nameof(command.Title), command.Title);
            ValidatorFactory.ValidateWidth(command.elementType, nameof(command.Width), command.Width);
            ValidatorFactory.ValidateHeight(command.elementType, nameof(command.Height), command.Height);

            return await this.applicationService.HandleCommand(command);

        }
        catch (Exception ex)
        {
            return new ApiResultDTO(false, ex.Message);
        }
    }


    [HttpPut("update")]

    public async ValueTask<ApiResultDTO> Update(UpdateDimensionCommand command)
    {
        try
        {
            ValidatorFactory.ValidateString(nameof(command.Id), command.Id);
            ValidatorFactory.ValidateName(nameof(command.Title), command.Title);
            ElementType elementType;
            Enum.TryParse(command.Id.Split('-')[0], out elementType);
            ValidatorFactory.ValidateWidth(elementType, nameof(command.Width), command.Width);
            ValidatorFactory.ValidateHeight(elementType, nameof(command.Height), command.Height);

            return await applicationService.HandleCommand(command);
        }
        catch (Exception ex)
        {
            return new ApiResultDTO(false, ex.Message);
        }
    }



}

