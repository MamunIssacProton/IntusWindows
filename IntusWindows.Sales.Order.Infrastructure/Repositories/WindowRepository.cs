using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Domain.Entities;
using IntusWindows.Sales.Order.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IntusWindows.Sales.Order.Infrastructure.Repositories;

public sealed class WindowRepository : BaseContextRepository, IWindowRepository
{
    public WindowRepository(Context context) : base(context)
    {
    }

    public async Task<ApiResultDTO> AddWindow(Window window)
    {
        await context.Windows.AddAsync(window);
        await context.SaveChangesAsync();
        return new ApiResultDTO(true, window.Id.ToString());
    }

    public async Task<IReadOnlyList<Window>> GetAllWindowsListAsync()
    {
        return context.Windows.Include(x => x.SubElements).ThenInclude(x => x.dimension).ToList().AsReadOnly();

    }
}

