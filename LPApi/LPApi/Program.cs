using LPApi.Models;
using LPApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string conn = builder.Configuration.GetConnectionString("conn");
builder.Services.AddDbContext<T3ShopContext>(options =>
{
    options.UseSqlServer(conn);
});
//injecting the service
builder.Services.AddScoped<IRepo<int, Product>, ProductRepo>();
builder.Services.AddScoped<IRepo<int, ShoppingCartItem>, CartRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
