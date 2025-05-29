using Application.Dtos;
using BuildingBlocks.CQRS;

namespace Application.Orders.Queries.GetOrdersByName;

public record GetOrdersByNameQuery(string Name) : IQuery<GetOrdersByNameResult>;

public record GetOrdersByNameResult(List<OrderDto> Orders);