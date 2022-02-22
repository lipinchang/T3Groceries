using T3PersonalWkSpcApp.Models;
using T3PersonalWkSpcApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//string conn = builder.Configuration.GetConnectionString("conn");
//builder.Services.AddDbContext<GroceryContext>(options =>
//{
//    options.UseSqlServer(conn);
//});
//injecting the service
builder.Services.AddScoped<IRepo<int, Product>, ProductRepo>();
builder.Services.AddScoped<IRepo<int, ShoppingCartItem>, CartRepo>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddSession();   // add session

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
