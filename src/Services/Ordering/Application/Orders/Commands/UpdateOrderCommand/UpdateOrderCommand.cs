using Application.Dtos;
using BuildingBlocks.CQRS;

namespace Application.Orders.Commands.UpdateOrderCommand;

public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);