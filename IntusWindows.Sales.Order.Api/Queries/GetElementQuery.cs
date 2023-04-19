using System;
namespace IntusWindows.Sales.Order.Api.Queries;

public class GetElementQuery
{
    public Guid Id { get; set; }

    public Guid WindowId { get; set; }

    public Guid ElementId { get; set; }
}

