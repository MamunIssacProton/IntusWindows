using IntusWindows.Sales.Contract.Enums;
using IntusWindows.Sales.Contract.Exceptions;
using IntusWindows.Sales.Contract.Models.Map;
using IntusWindows.Sales.Contract.Utils;

namespace IntusWindows.Sales.Contract.Utils;

public class ValidatorFactory
{
    public static void ValidateGuid(string propertyName, Guid value)
    {
        if (value == default)
            throw new InvalidValueException(propertyName, "value cannot be default");

        if (value == Guid.Empty)
            throw new InvalidValueException(propertyName, "value cannot be empty");
    }


    public static void ValidateString(string propertyName, string? value)
    {
        if (string.IsNullOrEmpty(value))
            throw new InvalidValueException(propertyName, "value cannot be null or empty");

        if (value.Length < 5)
            throw new InvalidValueException(propertyName, "value must contain minimum 5 characters");

    }

    public static void ValidateName(string propertyName, string value)
    {

        if (string.IsNullOrEmpty(value))
            throw new InvalidValueException(propertyName, "value cannot be null or empty");
        if (value.Length < 2)
            throw new InvalidValueException(propertyName, "value must contains 4 characters");

    }

    public static void ValidateWidth(ElementType elementType, string property, decimal value)
    {
        switch (elementType)
        {
            case ElementType.Window:
                if (value == decimal.MinusOne)
                    throw new InvalidValueException("window", $"{property} cannot be negative");

                if (value == decimal.Zero)
                    throw new InvalidValueException("window", $"{property} cannot be zero");

                if (value > Constrains.MaxWindowWidth)
                    throw new InvalidValueException("window", $"{property}  cannot be greater than {Constrains.MaxWindowWidth}");

                if (value < Constrains.MinWindowWidth)
                    throw new InvalidValueException("window", $"{property} cannot be less than {Constrains.MinWindowWidth}");
                break;

            case ElementType.Doors:

                if (value == decimal.MinusOne)
                    throw new InvalidValueException("door", $"{property} cannot be negative");

                if (value == decimal.Zero)
                    throw new InvalidValueException("door", $"{property} cannot be zero");

                if (value > Constrains.MaxDoorWidth)
                    throw new InvalidValueException("door", $"{property} cannot be greater than {Constrains.MaxDoorWidth}");

                if (value < Constrains.MinDoorWidth)
                    throw new InvalidValueException("door", $"{property} cannot be less than {Constrains.MinDoorWidth}");
                break;
            default:
                break;
        }
    }

    public static void ValidateHeight(ElementType elementType, string property, decimal value)
    {
        switch (elementType)
        {
            case ElementType.Window:
                if (value == decimal.MinusOne)
                    throw new InvalidValueException("window", $"{property} cannot be negative");

                if (value == decimal.Zero)
                    throw new InvalidValueException("window", $"{property} cannot be zero");

                if (value > Constrains.MaxWindowHeight)
                    throw new InvalidValueException("window", $"{property} cannot be greater than {Constrains.MaxWindowHeight}");

                if (value < Constrains.MinWindowHeight)
                    throw new InvalidValueException("window", $"{property} cannot be less than {Constrains.MinWindowHeight}");
                break;

            case ElementType.Doors:

                if (value == decimal.MinusOne)
                    throw new InvalidValueException("door", $"{property} cannot be negative");

                if (value == decimal.Zero)
                    throw new InvalidValueException("door", $"{property} cannot be zero");

                if (value > Constrains.MaxDoorHeight)
                    throw new InvalidValueException("door", $"{property} cannot be greater than {Constrains.MaxDoorHeight}");

                if (value < Constrains.MinDoorHeight)
                    throw new InvalidValueException("door", $"{property} cannot be less than {Constrains.MinDoorHeight}");
                break;
            default:
                break;
        }
    }

    public static void ValidateElementType(ElementType? elementType)
    {
        if (!elementType.HasValue)
            throw new InvalidValueException("Element Type", "cannot be null or empty");
    }

    public static void Validate(Mapper.Dimension dimension)
    {
        ValidateString(propertyName: nameof(dimension.Title), value: dimension.Title);

        ValidateWidth(elementType:dimension.ElementType, property: nameof(dimension.Width), value: dimension.Width);

        ValidateHeight(elementType: dimension.ElementType, property: nameof(dimension.Height), value: dimension.Height);



    }

    public static void Validate(Mapper.Element element)
    {

        ValidateString(propertyName: nameof(element.Name), value: element.Name);
        ValidateString(propertyName: nameof(element.Name), value: element.DimensionId);


    }

    public static void Validate(Mapper.State state)
    {
        ValidateName(propertyName: nameof(state.Name), value: state.Name);
       
    }

    public static void Validate(Mapper.Window window)
    {
        ValidateName(propertyName:nameof(window.Title), value: window.Title);

        foreach (var id in window.ElementIds)
        {
            ValidateGuid(propertyName: "ElementId", value: id);

        }
    }
}

