using Basket.API.Basket.DeleteBasket;
using Basket.API.Basket.GetBasket;
using Basket.API.Basket.StoreBasket;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter(null, config => config.WithModules(
    typeof(GetBasketEndpoints),
    typeof(StoreBasketEndpoints),
    typeof(DeleteBasketEndpoints)));

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<Program>();
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();

app.MapCarter();

app.Run();
