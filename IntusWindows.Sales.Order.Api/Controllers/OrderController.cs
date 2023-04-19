using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models.Map;
using IntusWindows.Sales.Order.Api.ApplicationServices;
using IntusWindows.Sales.Order.Api.Commands.Create;
using IntusWindows.Sales.Order.Api.Commands.Delete;
using IntusWindows.Sales.Order.Api.Commands.Update;
using IntusWindows.Sales.Order.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
namespace IntusWindows.Sales.Order.Api.Controllers;

[Route("api/[controller]"), ApiController]

public class OrderController : ControllerBase
{
    private readonly ApplicationService applicationService;
    public OrderController(ApplicationService service)
    {
        this.applicationService = service;
    }

    [HttpGet("{id}")]
    public async ValueTask<OrderDTO> GetOrderbyIdAsync(Guid id) => await this.applicationService.GetOrderByIdAsync(id);



    [HttpGet("list")]
    public async ValueTask<IReadOnlyList<OrderDTO>> GetOrderListAsync()
                                                         => await this.applicationService.GetOrdersListAsync();


    [HttpPost("generate")]
    public async ValueTask<ApiResultDTO> GenerateNewOrderAsync(CreateOrderCommand command)

                                                          => await this.applicationService.HandleCommand(command);


    [HttpPut("changeDimensions")]
    public async ValueTask<ApiResultDTO> ChangeDimensionInOrderAsync(List<Mapper.ChangeDimensionFormOrder> command)

                                                               => await this.applicationService.HandleCommand(command);


    [HttpPut("changeOrderName")]
    public async ValueTask<ApiResultDTO> ChangeOrderName(ChangeOrderNameCommand command)

                                                      => await this.applicationService.HandleCommand(command);


    [HttpPut("changeStateInOrder")]
    public async ValueTask<ApiResultDTO> ChangeStateInOrder(ChangeStateInOrderCommand command)
                                                        => await this.applicationService.HandleCommand(command);

    [HttpPost("deleteElementsFromOrder")]
    public async ValueTask<ApiResultDTO> DeleteElementsFromOrder(DeleteElementsFromOrderCommand command)

                                                         => await this.applicationService.HandleCommand(command);



    [HttpPost("window")]
    public async ValueTask<ApiResultDTO> DeleteWindowFromOrder(DeleteWindowFromOrderCommand command)

                                                         => await this.applicationService.HandleCommand(command);


    [HttpDelete("{orderId}")]

    public async ValueTask<ApiResultDTO> DeleteOrder(Guid orderId)
    {
        

        try
        {
            if (orderId != default)
            {
                var command = new DeleteOrderCommand()
                {
                    OrderId = orderId
                };
                return await this.applicationService.HandleCommand(command);
            }
            return new ApiResultDTO(false, "Order id cannot be default");
        }
        catch (Exception ex)
        {
            return new ApiResultDTO(false, ex.Message);
        }
    }
                             
}

