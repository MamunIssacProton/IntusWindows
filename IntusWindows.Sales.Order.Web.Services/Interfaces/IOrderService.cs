using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models.Map;

namespace IntusWindows.Sales.Order.Web.Services.Interfaces;

public interface IOrderService
{
    ValueTask<ApiResultDTO> GenerateNewOrder(Mapper.Order order);


    ValueTask<ApiResultDTO> ChangeOrderNameByIdAsync(Mapper.ChangeOrderName orderName);

    ValueTask<ApiResultDTO> ChangeStateInOrderByIdAsync(Mapper.ChangeStateInOrder order);

    ValueTask<ApiResultDTO> ChangeDimensionsFromOrderByIdAsync(List<Mapper.ChangeDimensionFormOrder> order);

    Task<ApiResultDTO> DeleteOrderByIdAsync(Mapper.DeleteOrder order);

    ValueTask<ApiResultDTO> DeleteWindowFromOrderAsync(Mapper.DeleteWindowsFromOrder order);

    ValueTask<ApiResultDTO> DeleteElementsFromOrderAsync(Mapper.DeleteElementsFromOrdr order);

    ValueTask<List<OrderDTO>> GetOrdersListAsync();
}

