using Basket.API.Basket.DeleteBasket;
using Basket.API.Basket.GetBasket;
using Basket.API.Basket.StoreBasket;
using Basket.API.Data;
using Discount.Grpc;
using Marten;
using BuildingBlocks.Messaging.MassTransit;
using Basket.API.Basket.CheckoutBasket;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter(null, config => config.WithModules(
    typeof(GetBasketEndpoints),
    typeof(StoreBasketEndpoints),
    typeof(CheckoutBasketEndpoint),
    typeof(DeleteBasketEndpoints)));

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<Program>();
});

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions(); //.InitializeWith<CatalogInitialData>();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddMessageBroker(builder.Configuration);
builder.Services.AddHealthChecks();


// builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
// {
//     options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
// });

var app = builder.Build();

app.MapCarter();
app.UseHealthChecks("/health");

app.Run();
