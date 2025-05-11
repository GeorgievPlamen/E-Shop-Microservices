using Catalog.API.Products.CreateProduct;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter(null, config => config.WithModule<CreateProductEndpoint>());
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<Program>());

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapCarter();

app.Run();
