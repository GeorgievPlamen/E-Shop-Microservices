using Catalog.API.Products.CreateProduct;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter(null, config => config.WithModule<CreateProductEndpoint>());
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapCarter();

app.Run();
