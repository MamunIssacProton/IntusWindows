using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Domain.Enums;
using IntusWindows.Sales.Order.Infrastructure.Interfaces;
using Ordr =IntusWindows.Sales.Order.Domain.Entities.Order;
namespace IntusWindows.Sales.Infrastructure.Interfaces;

public interface IOrderRepository: IBaseContextRepository
{
	ValueTask<ApiResultDTO> CreateNewOrderAsync(Ordr order);

	ValueTask<ApiResultDTO> ChangeOrderNameByIdAsync(Guid orderId, string orderName);

	ValueTask<ApiResultDTO> ChangeStateInOrderByIdAsync(Guid orderId, string desiredStateId);

	ValueTask<ApiResultDTO> ChangeDimensionInOrderByIdAsync(Guid orderId, Guid windowId, string existDimensionId,
														string desiredDimensionId);

	ValueTask<ApiResultDTO> DeleteOrderByIdAsync(Guid orderId);

	ValueTask<ApiResultDTO> DeleteWindowFromOrderAsync(Guid orderId, Guid windowId);

	ValueTask<ApiResultDTO> DeleteElementFromOrderAsync(Guid orderId, Guid windowId, Guid elementId);

	ValueTask<IReadOnlyList<Ordr>> GetOrdersListAsync();
}

