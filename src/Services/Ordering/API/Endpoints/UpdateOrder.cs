using Carter;
using MediatR;

namespace API.Endpoints;

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdateOrder request, ISender mediator) => await mediator.Send(request));
    }
}