using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Infrastructure.Interfaces;
using IntusWindows.Sales.Order.Domain.Entities;
using IntusWindows.Sales.Order.Domain.Enums;
using IntusWindows.Sales.Order.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace IntusWindows.Sales.Order.Infrastructure.Repositories;

public sealed class OrderRepository : BaseContextRepository, IOrderRepository
{
    public OrderRepository(Context context) : base(context)
    {
    }

    public async ValueTask<ApiResultDTO> ChangeDimensionInOrderByIdAsync(Guid orderId, Guid windowId,
                                                                    string existDimensionId, string desiredDimensionId)
    {
        Domain.Entities.Order order = await context.Orders.Include(x => x.Windows)
                                                          .ThenInclude(x => x.SubElements)
                                                          .ThenInclude(x => x.dimension)
                                                          .Where(x => x.Id == orderId)
                                                          .FirstOrDefaultAsync();

        if (order is null)
            return new ApiResultDTO(false, $"no order has found with id {orderId}");


        var dimension = await context.Dimensions.Where(x => x.Id == desiredDimensionId).FirstOrDefaultAsync();
        if (dimension is null)
            return new ApiResultDTO(false, $"no dimension has found with id : {desiredDimensionId}");


        Element element = order.Windows.Where(x => x.Id == windowId)
                                       .FirstOrDefault().SubElements
                                       .Where(x => x.dimension.Id == existDimensionId)
                                       .FirstOrDefault();


        if (element is null)
            return new ApiResultDTO(false, $"no element has found which cotains dimension id {existDimensionId}");
        element.ChangeDimension(dimension);


        context.Orders.Update(order);

        await context.SaveChangesAsync();
        return new ApiResultDTO(true, $"dimension successfully updated");
    }

    public async ValueTask<ApiResultDTO> ChangeOrderNameByIdAsync(Guid orderId, string orderName)
    {
        var order = await context.Orders.Where(x => x.Id == orderId).FirstOrDefaultAsync();
        if (order is null)
            return new ApiResultDTO(false, $"no order has found with id : {orderId}");

        order.UpdateOrderName(OrderName.Create(orderName));

        await context.SaveChangesAsync();



        return new ApiResultDTO(true, $"successfully order name changed to : {order.OrderName.Value}");
    }

    public async ValueTask<ApiResultDTO> ChangeStateInOrderByIdAsync(Guid orderId, string desiredStateId)
    {
        await context.SaveChangesAsync();
        return new ApiResultDTO(true, "");
    }

    public async ValueTask<ApiResultDTO> CreateNewOrderAsync(Domain.Entities.Order order)
    {
        await context.Orders.AddAsync(order);

        await context.SaveChangesAsync();
        return new ApiResultDTO(true, $"{order.OrderName.Value} has sucessfully created with id : {order.Id}");
    }

    public async ValueTask<ApiResultDTO> DeleteElementFromOrderAsync(Guid orderId, Guid windowId, Guid elementId)
    {
        var order = await context.Orders.Include(x => x.Windows)
                                        .ThenInclude(x => x.SubElements)
                                        .ThenInclude(x => x.dimension)
                                        .Where(x => x.Id == orderId)
                                        .FirstOrDefaultAsync();

        if (order is null)
            return new ApiResultDTO(false, $"no order has found with id : {orderId}");

        var window = order.Windows.Where(x => x.Id == windowId).FirstOrDefault();

        if (window is null)
            return new ApiResultDTO(false, $"no window has found with id : {windowId} on the order : {orderId}");

        Element element = window.SubElements.Where(x => x.Id == elementId).FirstOrDefault();
        if (element is null)
            return new ApiResultDTO(false, $"no element has found in window : {elementId}");

        window.SubElements.Remove(element);


        await context.SaveChangesAsync();

        return new ApiResultDTO(true, $"element has sucessfully removed from order {orderId}");
    }

    public async ValueTask<ApiResultDTO> DeleteOrderByIdAsync(Guid orderId)
    {
        var order = await context.Orders.Where(x => x.Id == orderId).FirstOrDefaultAsync();
        if (order is null)
            return new ApiResultDTO(false, $"no order has found with id : {orderId}");
        context.Orders.Remove(order);

        await context.SaveChangesAsync();
        return new ApiResultDTO(true, $"sucessfully deleted order-{order.Id}");
    }


    public async ValueTask<ApiResultDTO> DeleteWindowFromOrderAsync(Guid orderId, Guid windowId)
    {
        var order = await context.Orders.Include(x => x.Windows)
                                        .ThenInclude(x => x.SubElements)
                                        .ThenInclude(x => x.dimension)
                                        .Where(x => x.Id == orderId)
                                        .FirstOrDefaultAsync();

        if (order is null)
            return new ApiResultDTO(false, $"no order has found with id : {orderId}");

        var window = order.Windows.Where(x => x.Id == windowId).FirstOrDefault();
        if (window is null)
            return new ApiResultDTO(false, $"no windows has found in order with window id : {windowId}");

        order.Windows.Remove(window);

        await context.SaveChangesAsync();

        return new ApiResultDTO(true, $"successfully removed window {windowId} from order");
    }

    public async ValueTask<IReadOnlyList<Domain.Entities.Order>> GetOrdersListAsync()
    {
        return await this.context.Orders.Include(x => x.Windows)
                                        .ThenInclude(x => x.SubElements)
                                        .ThenInclude(x => x.dimension)
                                        .ToListAsync();
    }
}

