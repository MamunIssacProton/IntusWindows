using System;
using System.Diagnostics;
using System.Linq.Expressions;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Domain.Entities;
using IntusWindows.Sales.Order.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IntusWindows.Sales.Order.Infrastructure.Repositories;

public sealed class ElementRepository : BaseContextRepository, IElementRepository
{

    public ElementRepository(Context context) : base(context)
    {

    }

    public Task<ApiResultDTO> DeleteElementAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<Element>> GetElementsListAsync()
    {
        return context.Elements.Include(x => x.dimension).ToList().AsReadOnly();
    }

    public async Task<ApiResultDTO> SaveElementAsync(Element element, string dimensionId)
    {
        //var dimension = await context.Dimensions.Where(x => x.Id == dimensionId).FirstOrDefaultAsync();
        //if (dimension == null)
        //    return new ApiResultDTO(false,$"{dimensionId} has not found in Dimension");
        //element.ChangeDimension(dimension);
        await context.Elements.AddAsync(element);
        await context.SaveChangesAsync();
        return new ApiResultDTO(true, element.Id.ToString());
    }
    public async Task<Element?> GetElementByIdAsync(Guid id)
    {
        var data = context.Elements.Include(x => x.dimension).Where(x => x.Id == id).FirstOrDefault();
        //Console.WriteLine($"got data : {data.Id}");
        if (data != null)
            return data;
        //return new ElementDTO(null,null,null,null);
        return null;
    }

    public async Task<ElementDTO?> GetElementsDTOByIdAsync(Guid id)
    {
        var data = context.Elements.AsNoTracking().Where(x => x.Id == id).Include(x => x.dimension).Select(x => new
        {
            id = Guid.Empty,
            name = x.elementName,
            width = x.dimension.Width,
            height = x.dimension.Height
        }).FirstOrDefault();
        //Console.WriteLine($"got data : {data.Id}");
        if (data != null)
            return new ElementDTO(data.id, data.name, data.width, data.height);

        return null;

    }

}

