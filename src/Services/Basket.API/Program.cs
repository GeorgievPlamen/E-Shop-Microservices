using Basket.API.Basket.GetBasket;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter(null, config => config.WithModules(
    typeof(GetBasketEndpoints)));

var app = builder.Build();

app.MapCarter();

app.Run();
