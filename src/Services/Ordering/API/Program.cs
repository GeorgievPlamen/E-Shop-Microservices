using API;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi()
    .AddApplication()
    .AddInfra(builder.Configuration);

var app = builder.Build();

app.UseApiService();

app.Run();
