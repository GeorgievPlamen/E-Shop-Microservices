namespace Discount.API.Services;

public static class MapGrpcService
{
    public static void MapGrpcServices(this WebApplication app)
    {
        app.MapGrpcService<DiscountService>();
    }
}