using System;
namespace IntusWindows.Sales.Contract.DTOs;

public record WindowDTO(Guid Id, string windowName, int totalSubElements, int quantityOfWindows, List<ElementDTO>Elements)
{

};
