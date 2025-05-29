using Application.Data;
using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var rowsDeleted = await dbContext.Orders
            .Where(x => x.Id.Value == request.OrderId)
            .ExecuteDeleteAsync(cancellationToken);

        return new DeleteOrderResult(rowsDeleted > 0);
    }
}
