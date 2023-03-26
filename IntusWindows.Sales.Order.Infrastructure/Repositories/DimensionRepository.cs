using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Domain.Entities;
using IntusWindows.Sales.Order.Domain.ValueObjects;
using IntusWindows.Sales.Order.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IntusWindows.Sales.Order.Infrastructure.Repositories;

public sealed class DimensionRepository : BaseContextRepository, IDimensionRepository
{
    public DimensionRepository(Context context) : base(context)
    {
    }


    public async Task<IReadOnlyList<Dimension>> GetAllDimensionsListAsync()
    {
        return await context.Dimensions.ToListAsync();
    }

    public async Task<ApiResultDTO> SaveDimensionAsync(Dimension dimension)
    {
        await context.Dimensions.AddAsync(dimension);
        await context.SaveChangesAsync();
        return new ApiResultDTO(true, dimension.Id);
    }

    public async Task<Dimension?> GetDimensionByIdAsync(string id)
    {
        var dimension = await context.Dimensions.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (dimension != null)
            return dimension;

        return null;
    }

    public async Task<ApiResultDTO> UpdateDimensionAsync(string id, decimal height, decimal width, string title)
    {
        var dimension = await context.Dimensions.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (dimension is null)
            throw new Exception($"No dimension has found with id : {id}");

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

