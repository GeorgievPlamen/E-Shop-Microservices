using Application.Data;
using BuildingBlocks.CQRS;
using Domain.ValueObjects;

namespace Application.Orders.Commands.UpdateOrderCommand;

public class UpdateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = new OrderId(request.Order.Id);

        var order = await dbContext.Orders.FindAsync(orderId, cancellationToken);

        ArgumentNullException.ThrowIfNull(order);

        // UpdateOrderWithNewValues(order, request.Order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);
    }
}
