using IntusWindows.Sales.Order.Domain.Events.Window;
using IntusWindows.Sales.Order.Domain.Utils;
using IntusWindows.Sales.Order.Domain.ValueObjects;
using IntusWindows.Sales.Order.Domain.Exceptions;
using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Entities;

public class Window : AggregateRoot

{
    public Guid Id { get; private set; }

    public virtual List<Element> SubElements { get; private set; }


    public WindowName windowName { get; private set; }

    public int TotalSubElements { get; private set; }

    public int QuantityOfWindows { get; private set; }


    protected Window()
    {
    }

    public Window(WindowId id)
    {
        this.SubElements = new List<Element>();
        ApplyDomainEvent(new WindowCreated()
        {
            Id = id.value
        });
    }


    public void AddElementToWindow(Element element)
    {

        this.SubElements.Add(element);
        this.TotalSubElements = this.SubElements.Count;
        ApplyDomainEvent(new ElementAddedToWindow()
        {
            ElementId = element.Id,
            WindowId = this.Id
        });

    }

    public void SetQuantityOfWindows(int qauntity)
    {
        ApplyDomainEvent(new QuantityOfWinodwsUpdated()
        {
            Id = this.Id,
            Quantity = qauntity
        });
    }
    public void SetWindowName(WindowName windowName)
    {
        ApplyDomainEvent(new WindowNameChanged()
        {
            Id = this.Id,
            Name = windowName.Value
        });
    }


    protected override void ChangeStateByUsingDomainEvent(IDomainEvent domainEvent)
    {
        switch (domainEvent)

        {
            case ElementAddedToWindow e:
                Id = e.WindowId;

                break;

            case WindowCreated e:

                Id = e.Id;
                break;
            case QuantityOfWinodwsUpdated e:
                Id = e.Id;
                QuantityOfWindows = e.Quantity;

                break;

            case WindowNameChanged e:
                Id = e.Id;
                windowName = WindowName.Create(e.Name);

                break;
            default:
                break;
        }
    }

    protected override void ValidateState()
    {
        ValidatorFactory.ValidateGuid(nameof(WindowId), this.Id);

    }
}

