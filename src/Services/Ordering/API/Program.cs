using API;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi()
    .AddApplication(builder.Configuration)
    .AddInfra(builder.Configuration);

var app = builder.Build();

app.UseApiService();

app.Run();
