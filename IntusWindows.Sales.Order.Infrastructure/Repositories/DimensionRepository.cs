using System;
using System.Runtime.CompilerServices;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Domain.Entities;
using IntusWindows.Sales.Order.Domain.ValueObjects;
using IntusWindows.Sales.Order.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

[assembly: InternalsVisibleTo("IntusWindows.Sales.Order.Api")]
namespace IntusWindows.Sales.Order.Infrastructure.Repositories;

public sealed class DimensionRepository : BaseContextRepository, IDimensionRepository
{
    public DimensionRepository(Context context) : base(context)
    {
    }


    public async ValueTask<IReadOnlyList<DimensionDTO>> GetAllDimensionsListAsync()
    {
        return await context.Dimensions.AsNoTracking().Select(x=>new DimensionDTO(x.Id,x.Width,x.Height,x.Title)).ToListAsync();
    }
    
    public async ValueTask<ApiResultDTO> SaveDimensionAsync(Dimension dimension)
    {
        await context.Dimensions.AddAsync(dimension);
        await context.SaveChangesAsync();
        return new ApiResultDTO(true, dimension.Id);
    }

    public async ValueTask<Dimension?> GetDimensionByIdAsync(string id)
    {
        var dimension = await context.Dimensions.FirstOrDefaultAsync(x=>x.Id==id);
        if (dimension != null)
            return dimension;
        
        return null;
    }

    public async ValueTask<ApiResultDTO> UpdateDimensionAsync(string id, decimal height, decimal width, string title)
    {
        var dimension = await context.Dimensions.FirstOrDefaultAsync(x=>x.Id==id);
        if (dimension is null)
            throw new Exception($"No dimension was found with ID: {id} in the {nameof(UpdateDimensionAsync)} method.");

        if (dimension.Height != height)
        {
            dimension.SetHeight(Height.Create(dimension.elementType, height));
        }
        if (dimension.Width != width)
        {
            dimension.SetWidth(Width.Create(dimension.elementType, width));
        }

        dimension.SetTitle(DimensionTitle.Create(title));
        context.Update(dimension);
        await context.SaveChangesAsync();
        return new ApiResultDTO(true, $"successfully dimension {title} has updated");

    }
}

