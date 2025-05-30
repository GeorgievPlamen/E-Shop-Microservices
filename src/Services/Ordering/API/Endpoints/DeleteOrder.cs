using Application.Orders.Commands.DeleteOrder;
using Carter;
using MediatR;

namespace API.Endpoints;

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders", async (DeleteOrderCommand request, ISender mediator) => await mediator.Send(request));
    }
}