using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Api.ApplicationServices;
using IntusWindows.Sales.Order.Api.Commands.Create;
using IntusWindows.Sales.Order.Api.Commands.Delete;
using IntusWindows.Sales.Order.Api.Commands.Update;
using Microsoft.AspNetCore.Mvc;

namespace IntusWindows.Sales.Order.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
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

														  => await this.applicationService.HandleCommand(command);
	

	[HttpPut("changeDimension")]
	public async Task<ApiResultDTO> ChangeDimensionInOrderAsync(ChangeDimensionInOrderCommand command)

														       => await this.applicationService.HandleCommand(command);
	

	[HttpPut("changeOrderName")]
	public async Task<ApiResultDTO> ChangeOrderName(ChangeOrderNameCommand command)

													=> await this.applicationService.HandleCommand(command);
	

	[HttpDelete("element")]
	public async Task<ApiResultDTO> DeleteElementFromOrder(DeleteElementFromOrderCommand command)

														 =>  await this.applicationService.HandleCommand(command);



	[HttpDelete("window")]
	public async Task<ApiResultDTO> DeleteWindowFromOrder(DeleteWindowFromOrderCommand command)

														 => await this.applicationService.HandleCommand(command);


	[HttpDelete]

	public async Task<ApiResultDTO> DeleteOrder(DeleteOrderCommand command)

											=>  await this.applicationService.HandleCommand(command);
	

}

