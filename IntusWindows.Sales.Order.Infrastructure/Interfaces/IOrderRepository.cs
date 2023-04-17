using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models;
using IntusWindows.Sales.Contract.Models.Map;
using IntusWindows.Sales.Order.Domain.Enums;
using IntusWindows.Sales.Order.Infrastructure.Interfaces;
using Ordr =IntusWindows.Sales.Order.Domain.Entities.Order;
namespace IntusWindows.Sales.Infrastructure.Interfaces;

public interface IOrderRepository: IBaseContextRepository
{
	ValueTask<ApiResultDTO> CreateNewOrderAsync(Ordr order);

	ValueTask<ApiResultDTO> ChangeOrderNameByIdAsync(Guid orderId, string orderName);

	ValueTask<ApiResultDTO> ChangeStateInOrderByIdAsync(Guid orderId, Guid desiredStateId);

	ValueTask<ApiResultDTO> ChangeDimensionsFromOrderByIdAsync(List<Mapper.ChangeDimensionFormOrder> order);

	ValueTask<ApiResultDTO> DeleteOrderByIdAsync(Guid orderId);

	ValueTask<ApiResultDTO> DeleteWindowsFromOrderAsync(Guid orderId, List<Guid> windowIds);

	ValueTask<ApiResultDTO> DeleteElementsFromOrderAsync(Guid orderId, List<ElementNode> elements);

	ValueTask<IReadOnlyList<OrderDTO>> GetOrdersListAsync();
}

