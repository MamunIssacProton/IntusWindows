using IntusWindows.Sales.Order.Domain.Enums;
using IntusWindows.Sales.Order.Domain.Events.Dimension;
using IntusWindows.Sales.Order.Domain.Utils;
using IntusWindows.Sales.Order.Domain.ValueObjects;
using IntusWindows.Sales.Order.Domain.Exceptions;
using IntusWindows.Sales.Shared;

namespace IntusWindows.Sales.Order.Domain.Entities;

public class Dimension : AggregateRoot
{
    public string Id { get; private set; }

    public Height Height { get; private set; }

    public Width Width { get; private set; }

    public string Title { get; private set; }

    public ElementType elementType { get; private set; }





    protected Dimension() { }

    public Dimension(DimensionId id)
    {
        this.Id = id;
        Enum.TryParse(id.Value.Split('-')[0], out ElementType elementType);

    }

    public void SetHeight(Height height)
    {
        ApplyDomainEvent(new HeightUpdated()
        {
            Id = this.Id,
            Height = height
        });
    }

    public void SetWidth(Width width)
    {
        ApplyDomainEvent(new WidthUpdated()
        {
            Id = this.Id,
            Width = width
        });
    }


    public void SetTitle(DimensionTitle title)
    {
        ApplyDomainEvent(new DimensionTitleUpdated()
        {
            Id = this.Id,
            Title = title
        });
    }
    protected override void ChangeStateByUsingDomainEvent(IDomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case DimensionCreated d:
                Id = d.Id;

                break;

            case DimensionTitleUpdated d:
                Id = d.Id;
                Title = d.Title;

                break;

            case WidthUpdated d:
                Id = d.Id;
                Width = Width.Create(elementType, d.Width);

                break;

            case HeightUpdated d:
                Id = d.Id;
                Height = Height.Create(elementType, d.Height);

                break;
            default:
                break;
        }
    }

    protected override void ValidateState()
    {
        ValidatorFactory.ValidateString(nameof(DimensionId), this.Id);

    }
}

