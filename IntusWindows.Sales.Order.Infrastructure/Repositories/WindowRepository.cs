using System;
using System.Runtime.CompilerServices;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Domain.Entities;
using IntusWindows.Sales.Order.Domain.Exceptions;
using IntusWindows.Sales.Order.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

[assembly:InternalsVisibleTo("IntusWindows.Sales.Order.Api")]

namespace IntusWindows.Sales.Order.Infrastructure.Repositories;

public sealed class WindowRepository : BaseContextRepository, IWindowRepository
{
    public WindowRepository(Context context) : base(context)
    {
    }

    public async ValueTask<ApiResultDTO> AddWindow(Window window)
    {
        if (string.IsNullOrEmpty(window?.windowName?.Value))
            return new ApiResultDTO(false,"window name cannot be empty");
        await context.Windows.AddAsync(window);
        await context.SaveChangesAsync();
        return new ApiResultDTO(true, window.Id.ToString());
    }

    public async ValueTask<IReadOnlyList<WindowDTO>> GetAllWindowsListAsync()
    {
        return context.Windows.AsNoTracking()
                              .Include(x => x.SubElements)
                              .ThenInclude(x => x.dimension)
                              .Select(x=>new WindowDTO(x.Id,x.windowName,x.TotalSubElements,x.QuantityOfWindows,
                               new List<ElementDTO>(x.SubElements.Select(s=>new ElementDTO(s.Id,s.elementName,s.dimension.Width,s.dimension.Height,s.dimension.Id)))
                              ))
                              .ToList().AsReadOnly();

    }

    public async ValueTask<Window> GetWindowByIdAsync(Guid id)
    {
        var window = await context.Windows.FirstOrDefaultAsync(x=>x.Id==id);
        if (window is null)
            throw new NotFoundException($"no window has found with id {id}");
        return window;
    }
}

