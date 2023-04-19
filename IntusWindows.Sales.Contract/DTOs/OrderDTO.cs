using System;
namespace IntusWindows.Sales.Contract.DTOs;

public record OrderDTO(Guid Id, string OrderName, string State, List<WindowDTO> windows)
{
}

