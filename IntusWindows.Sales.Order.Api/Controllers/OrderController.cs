using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Api.ApplicationServices;
using IntusWindows.Sales.Order.Api.Commands.Create;
using IntusWindows.Sales.Order.Api.Commands.Update;
using Microsoft.AspNetCore.Mvc;

namespace IntusWindows.Sales.Order.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderController:ControllerBase
{
	private readonly ApplicationService applicationService;
	public OrderController(ApplicationService service)
	{
		this.applicationService = service;
	}

	[HttpGet("list")]
	public async Task<IReadOnlyList<Domain.Entities.Order>> GetOrderListAsync()
														 => await this.applicationService.GetOrdersListAsync();


	[HttpPost("create")]
	public async Task<ApiResultDTO> GenerateNewOrderAsync(CreateOrderCommand command)
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

	[HttpPut("changeDimension")]
	public async Task<ApiResultDTO> ChangeDimensionInOrderAsync(ChangeDimensionInOrderCommand command)
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

