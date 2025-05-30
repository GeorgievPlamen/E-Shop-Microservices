using Application.Orders.Queries.GetOrders;
using Carter;
using MediatR;

namespace API.Endpoints;

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async (GetOrdersQuery request, ISender mediator) => await mediator.Send(request));
    }
}