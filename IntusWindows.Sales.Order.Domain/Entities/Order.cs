using System;
using IntusWindows.Sales.Order.Domain.Events.Order;
using IntusWindows.Sales.Order.Domain.ValueObjects;
using IntusWindows.Sales.Shared;
using System.Linq;
using IntusWindows.Sales.Order.Domain.Utils;

namespace IntusWindows.Sales.Order.Domain.Entities;

public class Order:AggregateRoot
{
    public List<Window> Windows { get; private set; } = new();

    public OrderName OrderName { get; private set; }

    public string State { get; set; }

    public Guid Id { get; private set; }

    protected Order()
    {
    }
	public Order(OrderId id)
	{
        ApplyDomainEvent(new OrderCreated()

        {
            Id=id.Value
        });
	}

    public void AddWindowToWindows(Window window)
    {
        if (Windows.Exists(x => x.Id == window.Id))
            return;
        Windows.Add(window);
        ApplyDomainEvent(new WindowAddedToWindows()
        {
            OrderId=this.Id,
            WindowId=window.Id
        });
    }

    public void AssignStateToOrder(State state)
    {
        ApplyDomainEvent(new AssignedStateToOrder()
        {
            Id=this.Id,
            State=state.Name.Value
        });
    }

    public void UpdateOrderName(OrderName orderName)
    {
        ApplyDomainEvent(new OrderNameChanged()
        {
            Id=this.Id,
            Name=orderName.Value
        });
    }
    protected override void ChangeStateByUsingDomainEvent(IDomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case OrderCreated e:
                this.Id =OrderId.Create(e.Id);
                Console.WriteLine($"Order created with id : {e.Id}");
                break;


            case WindowAddedToWindows e:

                Console.WriteLine($"window with id {e.WindowId} added to {this.Id} order");
                break;

            case AssignedStateToOrder e:
                State = StateName.Create(e.State);
                Console.WriteLine($"{e.State} has assigned to {e.Id} order");
                break;

            case OrderNameChanged e:
                this.OrderName = OrderName.Create(e.Name);
                Console.WriteLine($"Updated order Name : {OrderName.Value}");
                break;

            default:
                break;
        }
    }

    protected override void ValidateState()
    {
        ValidatorFactory.ValidateGuid(nameof(OrderId),this.Id);
    }
}

