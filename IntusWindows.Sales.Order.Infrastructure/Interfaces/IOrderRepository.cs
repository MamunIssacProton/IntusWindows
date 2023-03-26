using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Domain.Enums;
using IntusWindows.Sales.Order.Infrastructure.Interfaces;
using Ordr =IntusWindows.Sales.Order.Domain.Entities.Order;
namespace IntusWindows.Sales.Infrastructure.Interfaces;

public interface IOrderRepository: IBaseContextRepository
{
	Task<ApiResultDTO> CreateNewOrderAsync(Ordr order);

	Task<ApiResultDTO> ChangeOrderNameByIdAsync(Guid orderId, string orderName);

	Task<ApiResultDTO> ChangeStateInOrderByIdAsync(Guid orderId, string desiredStateId);

	Task<ApiResultDTO> ChangeDimensionInOrderByIdAsync(Guid orderId, Guid windowId, string existDimensionId,
														string desiredDimensionId);

	Task<ApiResultDTO> DeleteOrderByIdAsync(Guid orderId);

	Task<ApiResultDTO> DeleteWindowFromOrderAsync(Guid orderId, Guid windowId);

	Task<ApiResultDTO> DeleteElementFromOrderAsync(Guid orderId, Guid windowId, string elementId);

	Task<IReadOnlyList<Ordr>> GetOrdersListAsync();
}

