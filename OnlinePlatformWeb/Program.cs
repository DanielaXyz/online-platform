using Microsoft.EntityFrameworkCore;
using OnlinePlatformWeb.Data;
using OnlinePlatformWeb.Services.Interfaces;
using ShoppingCartService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization();

builder.Services.AddDbContext<CartDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICartRepository, CartRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CartDbContext>();
    await db.Database.EnsureCreatedAsync();
}

app.Run();