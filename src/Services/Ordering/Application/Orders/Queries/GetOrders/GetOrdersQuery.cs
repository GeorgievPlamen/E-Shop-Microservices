using Application.Dtos;
using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;

namespace Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersResult>;
public record GetOrdersResult(PaginatedResult<OrderDto> PaginatedResult);