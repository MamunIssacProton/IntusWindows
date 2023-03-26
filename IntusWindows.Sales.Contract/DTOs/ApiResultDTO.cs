namespace IntusWindows.Sales.Contract.DTOs;

public record ApiResultDTO(bool Ok, string? Message) : BaseDTO;