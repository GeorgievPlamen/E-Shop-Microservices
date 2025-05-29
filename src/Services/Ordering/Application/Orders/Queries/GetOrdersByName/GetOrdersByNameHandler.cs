using Application.Data;
using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(x => x.OrderItems)
            .AsNoTracking()
            .Where(x => x.OrderName.Value.Contains(request.Name))
            .OrderBy(x => x.OrderName)
            .ToListAsync(cancellationToken: cancellationToken);

        var orderDtos = orders.ProjectToOrderDto();

        return new GetOrdersByNameResult(orderDtos);
    }
}
