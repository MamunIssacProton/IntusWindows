using System;
using System.Runtime.CompilerServices;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Domain.Entities;
using IntusWindows.Sales.Order.Domain.Exceptions;
using IntusWindows.Sales.Order.Domain.ValueObjects;
using IntusWindows.Sales.Order.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

[assembly: InternalsVisibleTo("IntusWindows.Sales.Order.Api")]
namespace IntusWindows.Sales.Order.Infrastructure.Repositories;

public sealed class StateRepository:BaseContextRepository, IStateRepository
{
	public StateRepository(Context context):base(context)
	{
	}

    public async ValueTask<ApiResultDTO> ChangeStateNameAsync(State state)
    {
        var result = await context.States.FindAsync(state.Id);
        if (result is null)
            throw new NotFoundException($"no state has found with id : {state.Id}");

        context.States.Update(state);
        await context.SaveChangesAsync();
        return new ApiResultDTO(true,"Successfully changed state name");
    }


    public async ValueTask<ApiResultDTO> CreateNewStateAsync(State state)
{
    if (string.IsNullOrEmpty(state?.Name?.Value))
        return new ApiResultDTO(false, "State name cannot be null or empty.");

    await context.States.AddAsync(state);

    try
    {
        await context.SaveChangesAsync(CancellationToken.None);
        return new ApiResultDTO(true, $"{state.Name.Value} saved successfully");
    }
    catch (Exception ex)
    {
        return new ApiResultDTO(false, $"Something went wrong while saving the state: {ex.Message}");
    }
}


    public async ValueTask<IReadOnlyList<StateDTO>> GetAllStatesListAsync()
    {
        var states=  await context.States.AsNoTracking().Select(x=>new StateDTO(x.Id,x.Name)).ToListAsync();
      
        return states.AsReadOnly();
    }

    public async ValueTask<State> GetStateByIdAsync(Guid id)
    {
       var state= await context.States.SingleOrDefaultAsync(x=>x.Id==id);
        if (state is null)
            throw new NotFoundException($"no state has found with id : {id}");
        return state;
    }
}

