using IntusWindows.Sales.Contract.DTOs;
using IntusWindows.Sales.Contract.Models.Map;
using IntusWindows.Sales.Infrastructure.Interfaces;
using IntusWindows.Sales.Order.Api.Commands;
using IntusWindows.Sales.Order.Api.Commands.Create;
using IntusWindows.Sales.Order.Api.Commands.Delete;
using IntusWindows.Sales.Order.Api.Commands.Update;
using IntusWindows.Sales.Order.Api.Queries;
using IntusWindows.Sales.Order.Domain.Entities;
using IntusWindows.Sales.Order.Domain.Enums;
using IntusWindows.Sales.Order.Domain.Events;
using IntusWindows.Sales.Order.Domain.Exceptions;
using IntusWindows.Sales.Order.Domain.ValueObjects;
using IntusWindows.Sales.Order.Infrastructure.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Ordr = IntusWindows.Sales.Order.Domain.Entities.Order;
namespace IntusWindows.Sales.Order.Api.ApplicationServices;

public class ApplicationService
{
    private readonly IDimensionRepository dimensionRepository;
    private readonly IElementRepository elementRepository;
    private readonly IWindowRepository windowRepository;
    private readonly IStateRepository stateRepository;
    private readonly IOrderRepository orderRepository;

    public ApplicationService(IDimensionRepository repository, IElementRepository elementRepository,
                              IWindowRepository windowRepository, IStateRepository stateRepository,
                              IOrderRepository orderRepository)
    {
        this.dimensionRepository = repository;
        this.elementRepository = elementRepository;
        this.windowRepository = windowRepository;
        this.stateRepository = stateRepository;
        this.orderRepository = orderRepository;

    }

    public async ValueTask<ApiResultDTO> HandleCommand(CreateElementCommand command)
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

       return await elementRepository.SaveElementAsync(element, command.DimensionId);


    }


    public async ValueTask<ApiResultDTO> HandleCommand(CreateDimensionCommand command)
    {
        var dimension = new Dimension(DimensionId.Create(command.elementType, command.Width, command.Height));
        dimension.SetHeight(Height.Create(command.elementType, command.Height));
        dimension.SetWidth(Width.Create(command.elementType, command.Width));
        dimension.SetTitle(DimensionTitle.Create(command.Title));

        return await dimensionRepository.SaveDimensionAsync(dimension);


    }

    public async ValueTask<ApiResultDTO> HandleCommand(CreateWindowCommand command)
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

    public async ValueTask<ApiResultDTO> HandleCommand(CreateStateCommand command)
    {
        State state = new State(StateId.Create(command.Id));
        state.ChangeStateName(StateName.Create(command.Name));

        return await this.stateRepository.CreateNewStateAsync(state);
    }



    public async ValueTask<ApiResultDTO> HandleCommand(CreateOrderCommand command)
    {
        Ordr order = new Ordr(OrderId.Create(command.Id));
        order.UpdateOrderName(OrderName.Create(command.OrderName));
        State state = await this.stateRepository.GetStateByIdAsync(command.StateId);
        if(state is null)
            throw new NotFoundException($"state has not found with id : {command.StateId}");

        order.AssignStateToOrder(state);

        foreach (var id in command.Windows)
        {
            var window = await this.windowRepository.GetWindowByIdAsync(id);
            if (window is null)
                throw new NotFoundException($"window has not found with id :{id}");

            order.AddWindowToWindows(window);
        }
        

        return await this.orderRepository.CreateNewOrderAsync(order);
    }



    public async ValueTask<ApiResultDTO> HandleCommand(List<Mapper.ChangeDimensionFormOrder> command)
    {
        return await this.orderRepository.ChangeDimensionsFromOrderByIdAsync(command);
    }

    public async ValueTask<ApiResultDTO> HandleCommand(ChangeStateInOrderCommand command)
    {
        return await this.orderRepository.ChangeStateInOrderByIdAsync(command.OrderId, command.StateId);
    }

    public async ValueTask<ApiResultDTO> HandleCommand(ChangeOrderNameCommand command)
    {

        return await this.orderRepository.ChangeOrderNameByIdAsync(OrderId.Create(command.Id), command.OrderName);
    }


    public async ValueTask<ElementDTO?> HandleQuery(GetElementQuery query)
                                   => await this.elementRepository.GetElementsDTOByIdAsync(query.Id);


    public async ValueTask<IReadOnlyList<WindowDTO>> GetAllWindowQuery() =>
                                             await this.windowRepository.GetAllWindowsListAsync();


    public async ValueTask<IReadOnlyList<OrderDTO>> GetOrdersListAsync() =>
                                           await this.orderRepository.GetOrdersListAsync();

    public async ValueTask<IReadOnlyList<DimensionDTO>> GetDimensionsAsync()

                                                  => await this.dimensionRepository.GetAllDimensionsListAsync();


    public async ValueTask<IReadOnlyList<StateDTO>> GetAllStateListAsync()
                                                 => await this.stateRepository.GetAllStatesListAsync();

    public async ValueTask<IReadOnlyList<ElementDTO>> GetAllElementListAsync()
                                                => await this.elementRepository.GetElementsListAsync();
    public async ValueTask<ApiResultDTO> HandleCommand(UpdateDimensionCommand command)
    {
        return await this.dimensionRepository
                         .UpdateDimensionAsync(command.Id, command.Height, command.Width, command.Title);
    }


    public async ValueTask<ApiResultDTO> HandleCommand(UpdateStateCommand command)
    {
        var state = await this.stateRepository.GetStateByIdAsync(command.Id);
        if (state is null)
            throw new NotFoundException($"state not found with id : {command.Id}");
        state.ChangeStateName(StateName.Create(command.Name));

        return await this.stateRepository.ChangeStateNameAsync(state);
    }


    public async ValueTask<ApiResultDTO> HandleCommand(DeleteElementsFromOrderCommand command)
    {
        return await this.orderRepository.DeleteElementsFromOrderAsync(command.OrderId,command.Elements);
    }

    public async ValueTask<ApiResultDTO> HandleCommand(DeleteOrderCommand command)
    {
       return await this.orderRepository.DeleteOrderByIdAsync(command.OrderId);
    }

    public async ValueTask<ApiResultDTO> HandleCommand(DeleteWindowFromOrderCommand command)
    {
        return await this.orderRepository.DeleteWindowsFromOrderAsync(command.OrderId,command.WindowIds);
    }
}


