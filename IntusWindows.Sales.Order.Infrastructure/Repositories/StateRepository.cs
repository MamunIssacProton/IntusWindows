using System;
using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Domain.Entities;
using IntusWindows.Sales.Order.Domain.Exceptions;
using IntusWindows.Sales.Order.Domain.ValueObjects;
using IntusWindows.Sales.Order.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IntusWindows.Sales.Order.Infrastructure.Repositories;

public class StateRepository:BaseContextRepository, IStateRepository
{
	public StateRepository(Context context):base(context)
	{
	}

    public async Task<ApiResultDTO> ChangeStateNameAsync(State state)
    {
        var result = await context.States.Where(x => x.Id == state.Id).FirstOrDefaultAsync();
        if (result is null)
            throw new NotFoundException($"no state has found with id : {state.Id}");

        context.States.Update(state);
        await context.SaveChangesAsync();
        return new ApiResultDTO(true,"Successfully changed state name");
    }


    public async Task<ApiResultDTO> CreateNewStateAsync(State state)

    {
        await context.States.AddAsync(state);
        await context.SaveChangesAsync();
        return new ApiResultDTO(true,$"{state.Name.Value} saved successfully");
    }

    public async Task<IReadOnlyList<State>> GetAllStatesListAsync()
    {
        var states=  await context.States.ToListAsync();
        return states.AsReadOnly();
    }

    public async Task<State> GetStateByIdAsync(Guid id)
    {
       var state= await context.States.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (state is null)
            throw new NotFoundException($"no state has found with id : {id}");
        return state;
    }
}

