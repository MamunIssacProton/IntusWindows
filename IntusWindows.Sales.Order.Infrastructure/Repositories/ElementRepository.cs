﻿using System;
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

    public ValueTask<ApiResultDTO> DeleteElementAsync(Guid id)
    {
        throw new NotImplementedException();
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
        //Console.WriteLine($"got data : {data.Id}");
        if (data != null)
            return data;
        //return new ElementDTO(null,null,null,null);
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
                                         
        //Console.WriteLine($"got data : {data.Id}");
        if (data != null)
            return new ElementDTO(data.id, data.name, data.width, data.height,data.dimensionId);

        return null;

    }

}

