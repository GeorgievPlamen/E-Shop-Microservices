using Application.Orders.Queries.GetOrdersByName;
using Carter;
using MediatR;

namespace API.Endpoints;

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/name", async (GetOrdersByNameQuery request, ISender mediator) => await mediator.Send(request));
    }
}