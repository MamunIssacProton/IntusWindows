using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Domain.Entities;
using IntusWindows.Sales.Order.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

[assembly: InternalsVisibleTo("IntusWindows.Sales.Order.Api")]

namespace IntusWindows.Sales.Order.Infrastructure.Repositories;

public sealed class ElementRepository : BaseContextRepository, IElementRepository
{

    public ElementRepository(Context context) : base(context)
    {

    }

    public async ValueTask<ApiResultDTO> DeleteElementAsync(Guid id)
    {
        var element = await context.Elements.FirstOrDefaultAsync(x => x.Id == id);
        if (element == null)
            return new ApiResultDTO(false, $"No element has found with id {id}");
        context.Elements.Remove(element);
        await context.SaveChangesAsync();
        return new ApiResultDTO(true, $"{element.elementName} has successfully deleted");
    }

    public async ValueTask<IReadOnlyList<ElementDTO>> GetElementsListAsync()
    {
        return context.Elements.AsNoTracking()
                               .Include(x => x.dimension)
                               .Select(x=> new ElementDTO(x.Id,x.elementName,x.dimension.Width,x.dimension.Height,x.dimension.Id))
                               .ToList()
                               .AsReadOnly();
    }

    public async ValueTask<ApiResultDTO> SaveElementAsync(Element element, string dimensionId)
    {
        await context.Elements.AddAsync(element);
        await context.SaveChangesAsync();
        return new ApiResultDTO(true, element.Id.ToString());
    }
    public async ValueTask<Element?> GetElementByIdAsync(Guid id)
    {
        var data = await context.Elements.AsNoTracking().Include(x => x.dimension).FirstOrDefaultAsync(x=>x.Id==id);
    
        if (data != null)
            return data;
        
        return null;
    }

    public async ValueTask<ElementDTO?> GetElementsDTOByIdAsync(Guid id)
    {
        var data = await context.Elements.AsNoTracking()
                                         .Where(x => x.Id == id)
                                         .Include(x => x.dimension)
                                         .Select(x => new
                                         {
                                             id = Guid.Empty,
                                             name = x.elementName,
                                             width = x.dimension.Width,
                                             height = x.dimension.Height,
                                             dimensionId=x.dimension.Id
                                         }).FirstOrDefaultAsync();
                                         
        
        if (data != null)
            return new ElementDTO(data.id, data.name, data.width, data.height,data.dimensionId);

        return null;

    }

}

