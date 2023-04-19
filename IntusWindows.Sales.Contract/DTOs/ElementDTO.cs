namespace IntusWindows.Sales.Contract.DTOs;

public record ElementDTO(Guid? Id, string? elementName, decimal width, decimal height, string? dimensionId=default);

