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
	public async ValueTask<IReadOnlyList<Domain.Entities.Order>> GetOrderListAsync()
														 => await this.applicationService.GetOrdersListAsync();


	[HttpPost("create")]
	public async ValueTask<ApiResultDTO> GenerateNewOrderAsync(CreateOrderCommand command)

														  => await this.applicationService.HandleCommand(command);
	

	[HttpPut("changeDimension")]
	public async ValueTask<ApiResultDTO> ChangeDimensionInOrderAsync(ChangeDimensionInOrderCommand command)

														       => await this.applicationService.HandleCommand(command);
	

	[HttpPut("changeOrderName")]
	public async ValueTask<ApiResultDTO> ChangeOrderName(ChangeOrderNameCommand command)

													=> await this.applicationService.HandleCommand(command);
	

	[HttpDelete("element")]
	public async ValueTask<ApiResultDTO> DeleteElementFromOrder(DeleteElementFromOrderCommand command)

														 =>  await this.applicationService.HandleCommand(command);



	[HttpDelete("window")]
	public async ValueTask<ApiResultDTO> DeleteWindowFromOrder(DeleteWindowFromOrderCommand command)

														 => await this.applicationService.HandleCommand(command);


	[HttpDelete]

	public async ValueTask<ApiResultDTO> DeleteOrder(DeleteOrderCommand command)

											=>  await this.applicationService.HandleCommand(command);
	

}

