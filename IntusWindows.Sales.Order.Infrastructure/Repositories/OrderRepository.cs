using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models;
using IntusWindows.Sales.Contract.Models.Map;
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

    public async ValueTask<ApiResultDTO> ChangeDimensionsFromOrderByIdAsync(List<Mapper.ChangeDimensionFormOrder> orders)
    {
        foreach (var Order in orders)
        {

            Domain.Entities.Order order = await context.Orders.Include(x => x.Windows)
                                                          .ThenInclude(x => x.SubElements)
                                                          .ThenInclude(x => x.dimension)
                                                          .Where(x => x.Id == Order.OrderId)
                                                          .SingleOrDefaultAsync();





            if (order is null)
                return new ApiResultDTO(false, $"no order has found with id {Order.OrderId}");


            var dimension = await context.Dimensions.SingleOrDefaultAsync(x=>x.Id==Order.NewDimensionId);
            if (dimension is null)
                return new ApiResultDTO(false, $"no dimension has found with id : {Order.NewDimensionId}");


            Domain.Entities.Element element = order.Windows.Where(x => x.Id == Order.WindowId)
                                           .FirstOrDefault().SubElements
                                           .FirstOrDefault(x => x.dimension.Id == Order.CurrentDimensionId);


            if (element is null)
                return new ApiResultDTO(false, $"no element has found which contains dimension id {Order.CurrentDimensionId}");
            element.ChangeDimension(dimension);


            context.Orders.Update(order);

            foreach (var a in order.Windows)
            {
                foreach (var s in a.SubElements)
                {
                     Console.WriteLine($"updated order : element {s.Id} with {s.dimension.Id}");
                }
              
            }
            
        }

  
        await context.SaveChangesAsync();
        return new ApiResultDTO(true, $"dimension successfully updated");
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
        var order = await context.Orders.Where(x => x.Id == orderId).SingleOrDefaultAsync();
        if (order is null)
            return new ApiResultDTO(false, "No order has found");

        var state = await context.States.Where(x => x.Id == desiredStateId).SingleOrDefaultAsync();
        if (state is null)
            return new ApiResultDTO(false, "no state has found");
        order.State = state.Name;

        context.Orders.Update(order);
        await context.SaveChangesAsync();
        return new ApiResultDTO(true, $"successfully changed the order state to {order.State}");
    }

    public async ValueTask<ApiResultDTO> CreateNewOrderAsync(Domain.Entities.Order order)
    {
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
       // using var tr = await context.Database.BeginTransactionAsync();

        if (order is null)
            return new ApiResultDTO(false, $"no order has found with id : {orderId}");

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

        await context.SaveChangesAsync();

        if (removedElements.Any())
        {
            return new ApiResultDTO(true, $"Elements have been successfully removed from order {orderId}: {string.Join(",", removedElements)}");
        }
        else
        {
            return new ApiResultDTO(false, $"No elements have been removed from order {orderId}");
        }

        //await context.SaveChangesAsync();

       
    }

    public async ValueTask<ApiResultDTO> DeleteOrderByIdAsync(Guid orderId)
    {
        var order = await context.Orders.Where(x => x.Id == orderId).SingleOrDefaultAsync();
        if (order is null)
            return new ApiResultDTO(false, $"no order has found with id : {orderId}");
        context.Orders.Remove(order);

        await context.SaveChangesAsync();
        return new ApiResultDTO(true, $"sucessfully deleted order-{order.Id}");
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

        var window = order.Windows.RemoveAll(x => windowIds.Contains(x.Id));


        await context.SaveChangesAsync();

        return new ApiResultDTO(true, $"successfully removed windows from order");
    }

    public async ValueTask<IReadOnlyList<OrderDTO>> GetOrdersListAsync()
    {
        return this.context.Orders.AsNoTracking()
                                   .Include(x => x.Windows)
                                   .ThenInclude(s => s.SubElements)
                                   .ThenInclude(d => d.dimension)
                                   .Select(x => new OrderDTO(x.Id, x.OrderName, x.State, x.Windows
                                   .Select(w => new WindowDTO(w.Id, w.windowName, w.TotalSubElements, w.QuantityOfWindows,
                                           w.SubElements.Select(se => new ElementDTO(se.Id, se.elementName, se.dimension.Width, se.dimension.Height, se.dimension.Id))
                                            .ToList()))
                                            .ToList()))
                                   .ToList();

    }
}

