using BuildingBlocks.Behaviours;
using BuildingBlocks.Exceptions.Handler;
using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.DeleteProduct;
using Catalog.API.Products.GetProductByCategory;
using Catalog.API.Products.GetProductById;
using Catalog.API.Products.GetProducts;
using Catalog.API.Products.UpdateProduct;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter(null, config => config.WithModules(
    typeof(CreateProductEndpoint),
    typeof(GetProductByIdEndpoint),
    typeof(GetProductByCategoryEndpoint),
    typeof(UpdateProductEndpoint),
    typeof(DeleteProductEndpoint),
    typeof(GetProductsEndpoint)));

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<Program>();
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddExceptionHandler<CustomeExceptionHandler>();

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options =>
{

});

app.Run();
