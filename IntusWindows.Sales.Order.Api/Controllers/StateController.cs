using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Api.ApplicationServices;
using IntusWindows.Sales.Order.Api.Commands.Create;
using IntusWindows.Sales.Order.Api.Commands.Update;
using Microsoft.AspNetCore.Mvc;

namespace IntusWindows.Sales.Order.Api.Controllers;

[Route("api/[controller]"), ApiController]
public class StateController:ControllerBase
{
	private readonly ApplicationService applicationService;
	public StateController(ApplicationService applicationService)
	{
		this.applicationService = applicationService;
	}

	[HttpPost("create")]
	public async ValueTask<ApiResultDTO> Create(CreateStateCommand command)
	{
		try
		{
			return await this.applicationService.HandleCommand(command);
		}
		catch (Exception ex)
		{
			return new ApiResultDTO(false, ex.Message);
		}

	}

	[HttpPut("update")]
	public async ValueTask<ApiResultDTO> Update(UpdateStateCommand command)
	{
		try
		{
			return await this.applicationService.HandleCommand(command);
		}
		catch (Exception ex)
		{
			return new ApiResultDTO(false, ex.Message);
		}
	}
}

