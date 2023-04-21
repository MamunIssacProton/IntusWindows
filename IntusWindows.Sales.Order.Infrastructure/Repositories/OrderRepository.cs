using System;
using System.Runtime.CompilerServices;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models;
using IntusWindows.Sales.Contract.Models.Map;
using IntusWindows.Sales.Infrastructure.Interfaces;
using IntusWindows.Sales.Order.Domain.Entities;
using IntusWindows.Sales.Order.Domain.Enums;
using IntusWindows.Sales.Order.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

[assembly: InternalsVisibleTo("IntusWindows.Sales.Order.Api")]
namespace IntusWindows.Sales.Order.Infrastructure.Repositories;

public sealed class OrderRepository : BaseContextRepository, IOrderRepository
{
    public OrderRepository(Context context) : base(context)
    {
    }

    public async ValueTask<ApiResultDTO> ChangeDimensionsFromOrderByIdAsync(List<Mapper.ChangeDimensionFormOrder> orders)
    {
        try
        {
            foreach (var Order in orders)
            {
                Domain.Entities.Order order = await context.Orders
                    .Include(x => x.Windows)
                    .ThenInclude(x => x.SubElements)
                    .ThenInclude(x => x.dimension)
                    .SingleOrDefaultAsync(x => x.Id == Order.OrderId);

                if (order is null)
                {
                    return new ApiResultDTO(false, $"No order has found with id {Order.OrderId}");
                }

                var dimension = await context.Dimensions
                    .SingleOrDefaultAsync(x => x.Id == Order.NewDimensionId);

                if (dimension is null)
                {
                    return new ApiResultDTO(false, $"No dimension has found with id : {Order.NewDimensionId}");
                }

                Domain.Entities.Element element = order.Windows
                    .Where(x => x.Id == Order.WindowId)
                    .FirstOrDefault()
                    ?.SubElements
                    .FirstOrDefault(x => x.dimension.Id == Order.CurrentDimensionId);

                if (element is null)
                {
                    return new ApiResultDTO(false, $"No element has found which contains dimension id {Order.CurrentDimensionId}");
                }

                element.ChangeDimension(dimension);
            }

            await context.SaveChangesAsync();

            return new ApiResultDTO(true, $"Dimension successfully updated");
        }
        catch (Exception ex)
        {
            return new ApiResultDTO(false, $"An error occurred while updating the dimensions: {ex.Message}");
        }
    }


    public async ValueTask<ApiResultDTO> ChangeOrderNameByIdAsync(Guid orderId, string orderName)
    {
        var order = await context.Orders.Where(x => x.Id == orderId).SingleOrDefaultAsync();
        if (order is null)
            return new ApiResultDTO(false, $"no order has found with id : {orderId}");

        order.UpdateOrderName(OrderName.Create(orderName));

        await context.SaveChangesAsync();



        return new ApiResultDTO(true, $"successfully order name changed to : {order.OrderName.Value}");
    }

    public async ValueTask<ApiResultDTO> ChangeStateInOrderByIdAsync(Guid orderId, Guid desiredStateId)
    {
        var order = await context.Orders.FindAsync(orderId);
        if (order is null)
            return new ApiResultDTO(false, "No order has found");

        var state = await context.States.SingleOrDefaultAsync(x => x.Id == desiredStateId);
        if (state is null)
            return new ApiResultDTO(false, "no state has found");
        order.State = state.Name;

        context.Orders.Update(order);
        await context.SaveChangesAsync();
        return new ApiResultDTO(true, $"successfully changed the order state to {order.State}");
    }

    public async ValueTask<ApiResultDTO> CreateNewOrderAsync(Domain.Entities.Order order)
    {
        if (order == null)
            return new ApiResultDTO(false, "Order cannot be null or empty");
        await context.Orders.AddAsync(order);

        await context.SaveChangesAsync();
        return new ApiResultDTO(true, $"{order.OrderName.Value} has sucessfully created with id : {order.Id}");
    }

    public async ValueTask<ApiResultDTO> DeleteElementsFromOrderAsync(Guid orderId, List<ElementNode> elements)
    {
        var order = await context.Orders.Include(x => x.Windows)
                                        .ThenInclude(x => x.SubElements)
                                        .ThenInclude(x => x.dimension)
                                        .SingleOrDefaultAsync(x=>x.Id==orderId);
       

        if (order is null)
            return new ApiResultDTO(false, $"no order has found with id : {orderId}");

        using var tr = await context.Database.BeginTransactionAsync();

        var windows = order.Windows.ToDictionary(x => x.Id);

        var errorElements = new List<Guid>();
        var removedElements = new List<Guid>();

        foreach (var element in elements)
        {
            if (!windows.TryGetValue(element.WindowId, out var window))
            {
                errorElements.Add(element.ElementId);
                continue;
            }
            var elementToRemove = window.SubElements.SingleOrDefault(x => x.Id == element.ElementId);
            if (elementToRemove == null)
            {
                errorElements.Add(element.ElementId);
                continue;
            }
            window.SubElements.Remove(elementToRemove);
            removedElements.Add(element.ElementId);
        }

        if (errorElements.Any())
            return new ApiResultDTO(false, $"Failed to remove elements with ids: {string.Join(",", errorElements)}");
        try
        {
            await context.SaveChangesAsync();
            await tr.CommitAsync();
            if (removedElements.Any())
            {
                return new ApiResultDTO(true, $"Elements have been successfully removed from order {orderId}: {string.Join(",", removedElements)}");
            }
            else
            {
                return new ApiResultDTO(false, $"No elements have been removed from order {orderId}");
            }
        }
        catch (Exception ex)
        {
            await tr.RollbackAsync();
            return new ApiResultDTO(false,$"Something went wrong while deleting deleting elements from order.");
        }
       
    }

    public async ValueTask<ApiResultDTO> DeleteOrderByIdAsync(Guid orderId)
    {
        var order = await context.Orders.Where(x => x.Id == orderId).SingleOrDefaultAsync();
        if (order is null)
            return new ApiResultDTO(false, $"no order has found with id : {orderId}");
        context.Orders.Remove(order);
        try
        {
            await context.SaveChangesAsync();
            return new ApiResultDTO(true, $"sucessfully deleted order-{order.Id}");
        }
        catch (Exception ex)
        {
            return new ApiResultDTO(false, $"something went wrong while deleting the order,{ex.Message}");
        }
        
        
    }


    public async ValueTask<ApiResultDTO> DeleteWindowsFromOrderAsync(Guid orderId, List<Guid> windowIds)
    {
        var order = await context.Orders.Include(x => x.Windows)
                                        .ThenInclude(x => x.SubElements)
                                        .ThenInclude(x => x.dimension)
                                        .Where(x => x.Id == orderId)
                                        .SingleOrDefaultAsync();

        if (order is null)
            return new ApiResultDTO(false, $"no order has found with id : {orderId}");

        var removedWindows = new List<Guid>();
        foreach (var window in order.Windows)
        {
            if (windowIds.Contains(window.Id))
            {
                context.Windows.Remove(window);
                removedWindows.Add(window.Id);
            }
        }

        await context.SaveChangesAsync();

        if (removedWindows.Any())
        {
            return new ApiResultDTO(true, $"Successfully removed windows {string.Join(",", removedWindows)} from order {orderId}");
        }
        else
        {
            return new ApiResultDTO(false, $"No windows have been removed from order {orderId}");
        }
    }

    public async ValueTask<OrderDTO> GetOrderByIdAsync(Guid orderId)
    {
        var order = await this.context.Orders.AsNoTracking()
                                        .Include(x => x.Windows)
                                   .ThenInclude(s => s.SubElements)
                                   .ThenInclude(d => d.dimension)
                                   .FirstOrDefaultAsync(x => x.Id == orderId);

        if (order != null)
        {
            return new OrderDTO(order.Id, order.OrderName, order.State, order.Windows
                       .Select(w => new WindowDTO(w.Id, w.windowName, w.TotalSubElements, w.QuantityOfWindows,
                               w.SubElements
                                .Select(se => new ElementDTO(se.Id, se.elementName, se.dimension.Width, se.dimension.Height, se.dimension.Id))
                                .ToList()))
                       .ToList());
        }

        throw new Exception($"no order has found with id : {orderId}");

    }

    public async ValueTask<IReadOnlyList<OrderDTO>> GetOrdersListAsync()
    {
        return  this.context.Orders.AsNoTracking()
                                   .Include(x => x.Windows)
                                   .ThenInclude(s => s.SubElements)
                                   .ThenInclude(d => d.dimension)
                                   .Select(x => new OrderDTO(x.Id, x.OrderName, x.State, x.Windows
                                   .Select(w => new WindowDTO(w.Id, w.windowName, w.TotalSubElements, w.QuantityOfWindows,
                                           w.SubElements.Select(se => new ElementDTO(se.Id, se.elementName, se.dimension.Width, se.dimension.Height, se.dimension.Id))
                                            .ToList()))
                                            .ToList()))
                                   .ToList().AsReadOnly();
        
    }
}

