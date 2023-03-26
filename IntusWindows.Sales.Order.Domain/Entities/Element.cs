using System.ComponentModel.DataAnnotations;
using IntusWindows.Sales.Order.Domain.Enums;
using IntusWindows.Sales.Order.Domain.Events.Element;
using IntusWindows.Sales.Order.Domain.Utils;
using IntusWindows.Sales.Order.Domain.ValueObjects;
using IntusWindows.Sales.Order.Domain.Exceptions;
using IntusWindows.Sales.Shared;
namespace IntusWindows.Sales.Order.Domain.Entities;

public class Element : AggregateRoot
{

    public string elementName { get; private set; }


    public virtual Dimension dimension { get; private set; }


    public ElementType elementType { get; set; }


    public Guid Id { get; private set; }


    protected Element()
    {

    }


    public Element(ElementId id)
    {
        ApplyDomainEvent(new ElementCreated()
        {
            Id = id.Value

        });
    }


    public void ChangeDimension(Dimension dimension)
    {
        this.dimension = dimension;

        ApplyDomainEvent(new DimensionChanged()
        {
            Id = dimension.Id,
            Height = dimension.Height,
            Width = dimension.Width
        });
    }

    public void SetElementName(ElementType elementType, ElementName name)
    {
        ApplyDomainEvent(new ElementNameChanged()

        {
            Id = this.Id,
            Name = $"{elementType} - {name.Value}"
        }); ;
    }
    public void SetElementType(ElementType eType) => this.elementType = eType;


    protected override void ChangeStateByUsingDomainEvent(IDomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case ElementCreated e:
                Id = e.Id;

                break;
            case ElementNameChanged e:
                Id = e.Id;
                elementName = e.Name;

                break;

            case DimensionChanged e:


                break;


            default:
                break;
        }
    }

    protected override void ValidateState()
    {

        ValidatorFactory.ValidateGuid(nameof(ElementId), this.Id);


    }
}

