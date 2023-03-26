using System;
using IntusWindows.Sales.Order.Domain.Events.State;
using IntusWindows.Sales.Order.Domain.Utils;
using IntusWindows.Sales.Order.Domain.ValueObjects;
using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Entities;

public class State : AggregateRoot
{
    public Guid Id { get; private set; }


    public StateName Name { get; private set; }

    private State()
    {
    }

    public State(StateId id)
    {
        ApplyDomainEvent(new StateCreated()
        {
            Id = id
        });
    }

    public void ChangeStateName(StateName stateName)
    {
        ApplyDomainEvent(new StateNameChanged()
        {  Id=this.Id,
           Name=stateName.Value
        });
    }

    protected override void ChangeStateByUsingDomainEvent(IDomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case StateCreated e:
                this.Id = e.Id;
                Console.WriteLine($"state created with id : {e.Id}");
                break;

            case StateNameChanged e:
                this.Id = e.Id;
                Name =StateName.Create(e.Name);
                Console.WriteLine($"state name changed to : {e.Name}");
                break;
            default:
                break;
        }
    }

    protected override void ValidateState()
    {
        ValidatorFactory.ValidateGuid(nameof(StateId), this.Id);
    }
}

