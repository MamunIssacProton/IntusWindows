using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Infrastructure.Interfaces;
using IntusWindows.Sales.Order.Domain.Entities;
using IntusWindows.Sales.Order.Domain.Enums;
using IntusWindows.Sales.Order.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace IntusWindows.Sales.Order.Infrastructure.Repositories
{
	public class OrderRepository:BaseContextRepository, IOrderRepository
	{
		public OrderRepository(Context context):base(context)
		{
		}

        public async Task<ApiResultDTO> ChangeDimensionInOrderByIdAsync(Guid orderId, Guid windowId,
                                                                        string existDimensionId, string desiredDimensionId)
        {
            Domain.Entities.Order order = await context.Orders.Include(x=>x.Windows)
                                                              .ThenInclude(x=>x.SubElements)
                                                              .ThenInclude(x=>x.dimension)
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
                return new ApiResultDTO(false,$"no element has found which cotains dimension id {existDimensionId}");
            element.ChangeDimension(dimension);


             context.Orders.Update(order);

            await context.SaveChangesAsync();
            return new ApiResultDTO(true, $"dimension successfully updated");
        }

        public Task<ApiResultDTO> ChangeOrderNameByIdAsync(Guid orderId, string orderName)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResultDTO> ChangeStateInOrderByIdAsync(Guid orderId, string desiredStateId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResultDTO> CreateNewOrderAsync(Domain.Entities.Order order)
        {
            await context.Orders.AddAsync(order);

            await context.SaveChangesAsync();
            return new ApiResultDTO(true,$"{order.OrderName.Value} has sucessfully created with id : {order.Id}");
        }

        public Task<ApiResultDTO> DeleteElementFromOrderAsync(Guid orderId, Guid windowId, string elementId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResultDTO> DeleteOrderByIdAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResultDTO> DeleteWindowFromOrderAsync(Guid orderId, Guid windowId)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Domain.Entities.Order>> GetOrdersListAsync()
        {
            return await this.context.Orders.Include(x => x.Windows)
                                            .ThenInclude(x => x.SubElements)
                                            .ThenInclude(x => x.dimension)
                                            .ToListAsync();
        }
    }
}

