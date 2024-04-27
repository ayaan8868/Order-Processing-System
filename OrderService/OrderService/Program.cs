using Microsoft.EntityFrameworkCore;
using OrderService.Context;
using OrderService.RepositoryServices.Interface;
using OrderService.RepositoryServices.Repository;
using OrderService.ServiceLayer.IServices;
using OrderService.ServiceLayer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Initialize the InventoryServiceClient (Grpc Client) with the configuration
var configuration = builder.Configuration;
InventoryServiceClient.Initialize(configuration);

//adding dependecy of services and repository
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddDbContext<OrderProcessingSystemDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBCONNECTION")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
