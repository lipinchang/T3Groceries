using GatewayAPI.Models;
using GatewayAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson(opts =>
    opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

string conn = builder.Configuration.GetConnectionString("conn");
builder.Services.AddDbContext<UserContext>(options =>
{
    options.UseSqlServer(conn);
});
builder.Services.AddScoped<IManageUser<UserDTO>, ManageUser>();
builder.Services.AddScoped<IGenerateToken<UserDTO>, GenerateToken>();
builder.Services.AddScoped<IRepo<int, Customer>, CustomerRepo>();
builder.Services.AddScoped<IRepo<int, ProductDTO>, ProductRepo>();
builder.Services.AddScoped<IRepo<int, ShoppingCartItemDTO>, CartRepo>();
builder.Services.AddScoped<UserRepo>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(otps =>
    {
        otps.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
            ValidateIssuerSigningKey = true,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//add this right below
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
