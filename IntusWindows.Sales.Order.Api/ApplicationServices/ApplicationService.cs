using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Order.Api.Commands;
using IntusWindows.Sales.Order.Api.Commands.Create;
using IntusWindows.Sales.Order.Api.Commands.Update;
using IntusWindows.Sales.Order.Api.Queries;
using IntusWindows.Sales.Order.Domain.Entities;
using IntusWindows.Sales.Order.Domain.Enums;
using IntusWindows.Sales.Order.Domain.Events;
using IntusWindows.Sales.Order.Domain.ValueObjects;
using IntusWindows.Sales.Order.Infrastructure.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace IntusWindows.Sales.Order.Api.ApplicationServices;

public class ApplicationService
{
    private readonly IDimensionRepository dimensionRepository;
    private readonly IElementRepository elementRepository;
    private readonly IWindowRepository windowRepository;

    public ApplicationService(IDimensionRepository repository, IElementRepository elementRepository, IWindowRepository windowRepository)
    {
        dimensionRepository = repository;
        this.elementRepository = elementRepository;
        this.windowRepository = windowRepository;



    }

    public async Task HandleCommand(CreateElementCommand command)
    {
        var dimension = await dimensionRepository.GetDimensionByIdAsync(command.DimensionId);
        if (dimension == null)
            throw new InvalidDataException("no dimension has found");
        var element = new Element(ElementId.Create(command.Id));
        ElementType elementType;
        Enum.TryParse(dimension.Id.Split('-')[0], out elementType);

        element.ChangeDimension(dimension);
        element.SetElementName(elementType, ElementName.Create(command.Name));
        element.SetElementType(elementType);

        await elementRepository.SaveElementAsync(element, command.DimensionId);


    }


    public async Task<ApiResultDTO> HandleCommand(CreateDimensionCommand command)
    {
        var dimension = new Dimension(DimensionId.Create(command.elementType, command.Width, command.Height));
        dimension.SetHeight(Height.Create(command.elementType, command.Height));
        dimension.SetWidth(Width.Create(command.elementType, command.Width));
        dimension.SetTitle(DimensionTitle.Create(command.Title));

        return await dimensionRepository.SaveDimensionAsync(dimension);


    }

    public async Task<ApiResultDTO> HandleCommand(CreateWindowCommand command)
    {
        Window window = new Window(WindowId.Create(command.Id));
        window.SetWindowName(WindowName.Create(command.Title));
        window.SetQuantityOfWindows(command.QuantityOfWindows);
        foreach (var item in command.ElementIds)
        {
            var element = await elementRepository.GetElementByIdAsync(item);
            if (element == null)
                throw new InvalidDataException($"no element has found with id : {item}");

            window.AddElementToWindow(element);

        }


        return await windowRepository.AddWindow(window);

    }

    public async Task<ElementDTO?> HandleQuery(GetElementQuery query)
                                   => await this.elementRepository.GetElementsDTOByIdAsync(query.Id);


    public async Task<IReadOnlyList<Window>> GetAllWindowQuery() =>
                                             await this.windowRepository.GetAllWindowsListAsync();


    public async Task<ApiResultDTO> HandleCommand(UpdateDimensionCommand command)
    {
        return await this.dimensionRepository
                         .UpdateDimensionAsync(command.Id, command.Height, command.Width, command.Title);
    }

}


