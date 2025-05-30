using API.Endpoints;
using Carter;

namespace API;

public static class DI
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddCarter(null, config => config.WithModules(
            typeof(CreateOrder),
            typeof(DeleteOrder),
            typeof(GetOrders),
            typeof(GetOrdersByName),
            typeof(UpdateOrder)));

        return services;
    }

    public static WebApplication UseApiService(this WebApplication app)
    {
        app.MapCarter();

        return app;
    }
}