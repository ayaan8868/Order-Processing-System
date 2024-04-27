using InventoryService.Context;
using InventoryService.RepositoryService.Interface;
using InventoryService.RepositoryService.Services;
using InventoryService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddScoped<IInvertoryRepository, InventoryRepository>();
builder.Services.AddDbContext<InvernotoryDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBCONNECTION")));
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<InventoryGrpcService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
