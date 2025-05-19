namespace API;

public static class DI
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {

        return services;
    }

    public static WebApplication UseApiService(this WebApplication app)
    {
        //app.MapCarter();

        return app;
    }
}