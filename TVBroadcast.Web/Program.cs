using Microsoft.EntityFrameworkCore;
using TVBroadcast.BLL.Services;
using TVBroadcast.DAL.Context;
using TVBroadcast.DAL.Repositories;
using TVBroadcast.Domain.IRepository;
using TVBroadcast.Domain.IServices;

var builder = WebApplication.CreateBuilder(args);

// 💾 Connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🧸 Register Services
builder.Services.AddScoped<IShowRepository, ShowRepository>();
builder.Services.AddScoped<IShowService, ShowService>();

// 💖 Add MVC support
builder.Services.AddControllersWithViews();

// 🍼 Add session support BEFORE app is built
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// 🐥 Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🍬 Session middleware must come BEFORE authorization
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Schedule}/{action=Index}/{id?}");

app.Run();
