using Application.Orders.Commands.CreateOrder;
using Carter;
using MediatR;

namespace API.Endpoints;

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderCommand request, ISender mediator) => await mediator.Send(request));
    }
}