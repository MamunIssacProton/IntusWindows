using System;
namespace IntusWindows.Sales.Contract.DTOs;

public record ApiListResultDTO<T>(IReadOnlyList<T> List, int Total) : ApiResultDTO(true, null) where T : BaseDTO;


